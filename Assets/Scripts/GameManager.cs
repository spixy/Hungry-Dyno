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
    private Dyno dyno;
    private bool alive = true;

    [SerializeField]
    private AutoSpawner cloudSpawner;

    [SerializeField]
    private float hpDecay = 20f;

	[SerializeField]
	private GUI gui;
 
    public float Hp { get; private set; }

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
        Hp = 100;
        State = State.MainMenu;

        dynoStartingPosition = dyno.transform.position;

        PauseGame();
    }

    void Update() {
        if (alive) {
            if (Hp < 0) {
                dyno.Die();
                alive = false;
            }

            Hp -= Time.deltaTime * hpDecay;
        }
    }

	public void RegisterSpawner(ISpawner spawner)
	{
		this.spawners.Add (spawner);
	}

    public void UpdateHP(int diff) {
        Hp += diff;
    }

    public void EnableGodmode() {
        Debug.Log("GODMODE!!!");
        dyno.Godmode();
    }

    public void EnableBerserk() {
        Debug.Log("BERSERK!!!");
        dyno.Berserk();
    }

    public void StartGame()
    {
        this.dyno.Activate();
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
