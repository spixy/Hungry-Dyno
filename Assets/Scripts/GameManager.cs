using UnityEngine;

/// <summary>
/// Hlavny manazer hry (singleton)
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Dyno dyno;

    [SerializeField]
    private TrackManager trackManager;

    private static GameManager instance;

    /// <summary>
    /// Vrati instanciu na game manager singleton (http://wiki.unity3d.com/index.php/Singleton)
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (GameManager) FindObjectOfType(typeof(GameManager));
            }

            return instance;
        }
    }

    /// <summary>
    /// Nejake ziskane body
    /// </summary>
    public int Score { get; set; }

    // Use this for initialization
    void Start()
    {
        this.Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
