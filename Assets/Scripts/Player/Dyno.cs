using UnityEngine;

/// <summary>
/// Pohyb dyna
/// </summary>
public class Dyno : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private const float baseDur = 10f;
    [SerializeField]
    private float godmodeDur = baseDur;
    [SerializeField]
    private float berserkDur = baseDur;

    public bool godmode = false;
    public bool berserk = false;

    [SerializeField]
    private Attack attack;

    void Update()
    {
        if (this.transform.position.y < -8f)
        {
            this.Die();
            return;
        }

        if (godmode) {
            godmodeDur -= Time.deltaTime;
        }

        if (berserk) {
            berserkDur -= Time.deltaTime;
        }

        if (godmodeDur < 0) {
            godmode = false;
            godmodeDur = baseDur;
            Debug.Log("Godmode off.");
        }

        if (berserkDur < 0) {
            berserk = false;
            attack.SetBerserk(false);
            berserkDur = baseDur;
            Debug.Log("Berserk off.");
        }

        // 1 meter = 1 bod ?
        GameManager.Instance.Score = (int) this.transform.position.x;
    }

    public void Activate() {
        this.gameObject.SetActive(true);
    }

    public void Die() {
        Debug.Log("Dead!");
        this.gameObject.SetActive(false);
        GameManager.Instance.ExitGame();
    }

    private void UpdateHP(int diff) {
        GameManager.Instance.UpdateHP(diff);
    }

    public void Berserk() {
        attack.SetBerserk(true);
        berserk = true;
    }

    public void Godmode() {
        godmode = true;
    }
}
