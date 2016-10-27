using UnityEngine;

/// <summary>
/// Pohyb dyna
/// </summary>
public class Dyno : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private Bonus activeBonus;

    void Start()
    {
        this.activeBonus = Bonus.None;
    }

    void Update()
    {
        // 1 meter = 1 bod ?
        GameManager.Instance.Score = (int) this.transform.position.x;
    }

    private void OnBonusStarted(Bonus bonus)
    {
        this.activeBonus = bonus;

        // bla bla
    }

    private void OnBonusEnded()
    {
        this.activeBonus = Bonus.None;

        // bla bla
    }

}
