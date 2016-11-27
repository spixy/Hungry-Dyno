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

    [SerializeField]
    private Attack attack;

    [SerializeField]
    private float hpDecay = 20f;

    public bool Alive { get; private set; }

    public float Hp { get; private set; }

    public bool Godmode { get; set; }

    public bool Berserk { get; set; }

    public bool Attacking { get; set; }

    public void StartGame()
    {
        if (Godmode)
        {
            DisableGodmode();
        }
        if (Berserk)
        {
            DisableBerserk();
        }

        Hp = 100;
        Alive = true;
        Attacking = false;
    }

    void Update()
    {
        if (!Alive)
            return;

        if (Godmode) {
            godmodeDur -= Time.deltaTime;

            if (godmodeDur < 0)
            {
                DisableGodmode();
            }
        }

        if (Berserk) {
            berserkDur -= Time.deltaTime;

            if (berserkDur < 0)
            {
                DisableBerserk();
            }
        }

        // 1 meter = 0.5 boda
		GameManager.Instance.Score = (int) (transform.position.x * 0.5f);

        Hp -= Time.deltaTime * hpDecay;

        if (Hp <= 0 || transform.position.y < -8f)
        {
            Die();
        }
    }

    public void UpdateHP(int diff)
    {
        Hp = Mathf.Clamp(Hp + diff, 0f, 100f);
    }

    public void Die() {
        Alive = false;
        Debug.Log("Dead!");
        GameManager.Instance.ExitGame();
    }

    public void EnableBerserk()
    {
        if (!Godmode)
        {
            Debug.Log("BERSERK!!!");
            anim.SetBool("Berserk", true);
            attack.SetBerserk(true);
            berserkDur = baseDur;
            Berserk = true;
        }
    }

    public void EnableGodmode()
    {
        if (!Berserk)
        {
            Debug.Log("GODMODE!!!");
            anim.SetBool("Godmode", true);
            godmodeDur = baseDur;
            Godmode = true;
        }
    }

    private void DisableBerserk()
    {
        Berserk = false;
        attack.SetBerserk(false);
        berserkDur = baseDur;
        anim.SetBool("Berserk", false);
        Debug.Log("Berserk off.");
    }

    private void DisableGodmode()
    {
        Godmode = false;
        godmodeDur = baseDur;
        anim.SetBool("Godmode", false);
        Debug.Log("Godmode off.");
    }
}
