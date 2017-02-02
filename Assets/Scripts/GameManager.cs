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
    private Dyno dyno;

    [SerializeField]
    private AutoSpawner cloudSpawner;

	[SerializeField]
	private GUI gui;

    [SerializeField]
    private PoolManager poolManager;

    [SerializeField]
    private AudioSource soundtrack;

    [SerializeField]
    public Sfx sfx;
	
	[SerializeField]
	public CameraRunner cameraRunner;

	private Vector3 dynoStartingPosition;

	public ScoreStorage scoreStorage { get; private set; }

	/// <summary>
	/// Vrati instanciu na game manager singleton
	/// </summary>
	public static GameManager Instance { get; private set; }

	public GUI Gui
	{
		get { return this.gui; }
	}

	public Dyno Dyno
	{
		get { return this.dyno; }
	}

	public PoolManager PoolManager
	{
		get { return this.poolManager; }
	}

	/// <summary>
	/// Pozicia dyna
	/// </summary>
	public Vector3 dynoPosition
    {
        get { return this.Dyno.transform.position; }
    }

	/// <summary>
	/// Meno hraca
	/// </summary>
	public string PlayerName { get; set; }

	/// <summary>
    /// Ziskane body
    /// </summary>
    public float Score { get; set; }

	/// <summary>
	/// Najvyssie skore
	/// </summary>
	public int MaxScore
	{
		get
		{
			int value = scoreStorage.GetScore(PlayerName);

			if (Score > value)
			{
				value = (int)Score;
				MaxScore = value;
			}

			return value;
		}
		private set
		{
			scoreStorage.SetScore(PlayerName, value, true);
		}
	}

    /// <summary>
    /// Stav hry
    /// </summary>
    public State State { get; private set; }

    private void Awake()
    {
        Instance = this;

		Score = 0;
		State = State.MainMenu;
		scoreStorage = new ScoreStorage();

		soundtrack.Play();
	}

	void Start()
    {      
        dynoStartingPosition = Dyno.transform.position;

        PauseGame();
    }

    public void StartGame()
    {
        Score = 0;
        this.Dyno.StartGame();
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
		PlayerPrefs.Save();

		StartCoroutine(ResetCoroutine());
    }

	private IEnumerator ResetCoroutine()
	{
		this.Dyno.transform.position = this.dynoStartingPosition;

		this.PoolManager.Reset();

		// pockat 1 frame na repozicovanie kamery
		yield return null;

		//this.platformSpawner.GenerateStartingPlatform();

		this.Gui.ShowMenu();
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
        soundtrack.volume = 0.7f;
    }
}
