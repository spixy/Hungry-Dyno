using UnityEngine;

/// <summary>
/// Hlavny manazer hry (singleton)
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Dyno dyno;

    private Vector3 dynoStartingPosition;

    /// <summary>
    /// Vrati instanciu na game manager singleton
    /// </summary>
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Ziskane body
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Som v hre (nie som mrtvy, neni pauznuta hra, atd)
    /// </summary>
    public bool InGame { get; set; }

    void Start()
    {
        this.Score = 0;
        this.InGame = true;

        this.dynoStartingPosition = this.dyno.transform.position;

        this.PauseGame();
    }

    public void StartGame()
    {
        this.UnpauseGame();
    }

    public void RestartGame()
    {
        this.dyno.transform.position = this.dynoStartingPosition;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
}
