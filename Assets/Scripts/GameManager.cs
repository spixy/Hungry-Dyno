using System.Collections;
using UnityEngine;

public enum State
{
    MainMenu,
    InGame,
    Paused,
    DeadMenu
}

/// <summary>
/// Hlavny manazer hry (singleton)
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Dyno dyno;

    [SerializeField]
    private AutoSpawner cloudSpawner;

	[SerializeField]
	public GUI gui;

    [SerializeField]
    public PoolManager poolManager;

    [SerializeField]
    public AudioSource soundtrack;

    [SerializeField]
    public Sfx sfx;

    private Vector3 dynoStartingPosition;

	/// <summary>
	/// Vrati instanciu na game manager singleton
	/// </summary>
	public static GameManager Instance { get; private set; }

    /// <summary>
    /// Pozicia dyna
    /// </summary>
    public Vector3 dynoPosition
    {
        get { return this.dyno.transform.position; }
    }

    private float _Score;
    /// <summary>
    /// Ziskane body
    /// </summary>
    public float Score
	{
		get {
			return _Score;
		}
		set {
			_Score = value;

			if (MaxScore < value)
				MaxScore = value;
		}
	}

	/// <summary>
	/// Najvyssie skore
	/// </summary>
	public float MaxScore { get; private set; }

    /// <summary>
    /// Stav hry
    /// </summary>
    public State State { get; set; }


    private void Awake()
    {
        Instance = this;
	}

	void Start()
    {
        Score = 0;
        State = State.MainMenu;
        soundtrack.Play();

        dynoStartingPosition = dyno.transform.position;

        PauseGame();
    }

    public void StartGame()
    {
        Score = 0;
        this.dyno.StartGame();
        this.State = State.InGame;
        this.UnpauseGame();
    }

    public void RestartGame()
    {
        this.StartGame();
    }

    public void ExitGame()
    {
        this.State = State.MainMenu;
        Time.timeScale = 0f;

	    StartCoroutine(ResetCoroutine());
    }

	private IEnumerator ResetCoroutine()
	{
		this.dyno.transform.position = this.dynoStartingPosition;

		this.poolManager.Reset();

		// pockat 1 frame na repozicovanie kamery
		yield return null;

		//this.platformSpawner.GenerateStartingPlatform();

		this.gui.ShowMenu();
	}

	public void PauseGame()
    {
        this.State = State.Paused;
        Time.timeScale = 0f;
        soundtrack.volume = 0.3f;
    }

    public void UnpauseGame()
    {
        this.State = State.InGame;
        Time.timeScale = 1f;
        soundtrack.volume = 0.75f;
    }
}
