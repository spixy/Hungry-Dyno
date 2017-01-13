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
	private GUI gui;

	[SerializeField]
	public PoolManager poolManager;


    [SerializeField]
    private PlatformSpawner platformSpawner;

    private Vector3 dynoStartingPosition;

	public readonly TouchController touchController = new TouchController();

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

	public bool IsTap
	{
		get
		{
#if UNITY_EDITOR
			return Input.anyKeyDown;
#elif UNITY_ANDROID
			return touchController.IsTap();
#endif
		}
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
        this.cloudSpawner.StopSpawning();

	    StartCoroutine(ResetCoroutine());
    }

	private IEnumerator ResetCoroutine()
	{
		this.dyno.transform.position = this.dynoStartingPosition;

		this.poolManager.Reset();

		// pockat 1 frame na repozicovanie kamery
		yield return null;

		this.platformSpawner.GenerateStartingPlatform();

		this.gui.ShowMenu();
	}

	public void PauseGame()
    {
        this.State = State.Paused;
        Time.timeScale = 0f;
        this.cloudSpawner.StopSpawning();
    }

    public void UnpauseGame()
    {
        this.State = State.InGame;
        Time.timeScale = 1f;
        this.cloudSpawner.StartSpawning();
    }
}
