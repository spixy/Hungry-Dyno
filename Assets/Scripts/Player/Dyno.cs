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

    private bool godmode = false;
    private bool berserk = false;

    [SerializeField]
    private Attack attack;

    void Update()
    {
        if (godmode) {
            godmodeDur -= Time.deltaTime;
        }

        if (berserk) {
            berserkDur -= Time.deltaTime;
        }

        if (godmodeDur < 0) {
            godmode = false;
            godmodeDur = baseDur;
            anim.SetBool("Godmode", false);
            Debug.Log("Godmode off.");
        }

        if (berserkDur < 0) {
            berserk = false;
            attack.SetBerserk(false);
            berserkDur = baseDur;
            anim.SetBool("Berserk", false);
            Debug.Log("Berserk off.");
        }

        // 1 meter = 0.5 boda
		GameManager.Instance.Score = (int) (transform.position.x * 0.5f);

        if (transform.position.y < -8f) {
            Die();
        }
    }

    public void Activate() { }

    public bool HasGodmode() {
        return godmode;
    }

    public void Die() {
        Debug.Log("Dead!");
        GameManager.Instance.ExitGame();
    }

    public void Berserk() {
        if (!godmode) {
            Debug.Log("BERSERK!!!");
            anim.SetBool("Berserk", true);
            attack.SetBerserk(true);
            berserkDur = baseDur;
            berserk = true;
        }
    }

    public void Godmode() {
        if (!berserk) {
            Debug.Log("GODMODE!!!");
            anim.SetBool("Godmode", true);
            godmodeDur = baseDur;
            godmode = true;
        }
    }
}
