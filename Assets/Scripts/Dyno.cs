using UnityEngine;

/// <summary>
/// Pohyb dyna
/// </summary>
public class Dyno : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    void Start()
    {
    }
    
    void Update()
    {
        // 1 meter = 1 bod ?
        GameManager.Instance.Score = (int) this.transform.position.x;
    }
}
