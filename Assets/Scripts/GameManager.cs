using UnityEngine;
using System.Collections.Generic;

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
public class GameManager : MonoBehaviour {
    [SerializeField]
    public Dyno dyno;

    [SerializeField]
    private AutoSpawner cloudSpawner;

	[SerializeField]
	private GUI gui;

	[SerializeField]
	public PoolManager poolManager;
 
    private Vector3 dynoStartingPosition;

	private List<ISpawner> spawners = new List<ISpawner>();

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


    private int _Score;
    /// <summary>
    /// Ziskane body
    /// </summary>
    public int Score
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
	public int MaxScore { get; private set; }

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

	public void RegisterSpawner(ISpawner spawner)
	{
		this.spawners.Add (spawner);
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

		this.dyno.transform.position = this.dynoStartingPosition;

		foreach (ISpawner spawner in this.spawners)
			spawner.Reset ();

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
