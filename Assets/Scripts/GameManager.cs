using UnityEngine;

/// <summary>
/// Hlavny manazer hry (singleton)
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    /// <summary>
    /// Vrati instanciu na game manager singleton
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
    /// Ziskane body
    /// </summary>
    public int Score { get; set; }
    
    void Start()
    {
        this.Score = 0;
    }
    
}
