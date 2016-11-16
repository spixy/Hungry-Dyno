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
public class GameManager : MonoBehaviour {
    [SerializeField]
    private Dyno dyno;
    private bool alive = true;

    [SerializeField]
    private AutoSpawner cloudSpawner;

    [SerializeField]
    private float hpDecay = 20f;
 
    public float Hp { get; private set; }

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

    /// <summary>
    /// Ziskane body
    /// </summary>
    public int Score { get; set; }

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
        this.dyno.transform.position = this.dynoStartingPosition;
        this.StartGame();
    }

    public void ExitGame()
    {
        this.State = State.MainMenu;
        Time.timeScale = 0f;
        this.cloudSpawner.StopSpawning();
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
