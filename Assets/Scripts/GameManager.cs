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

    private static GameManager _instance;

    /// <summary>
    /// Vrati instanciu na game manager singleton
    /// </summary>
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (GameManager) FindObjectOfType(typeof(GameManager));
            }

            return _instance;
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
