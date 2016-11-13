using UnityEngine;
using UnityStandardAssets._2D;

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
        if (this.transform.position.y < -8f)
        {
            this.Die();
            return;
        }

        // 1 meter = 1 bod ?
        GameManager.Instance.Score = (int) this.transform.position.x;
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.ExitGame();
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
