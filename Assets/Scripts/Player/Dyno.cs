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

	[SerializeField]
	private Vector3 startPos;

	private float lastPosX = 0f;

    public bool Alive { get; private set; }

    public float Hp { get; private set; }

    public bool Godmode { get; set; }

    public bool Berserk { get; set; }

    public bool Attacking { get; set; }

	void Awake()
	{
		ResetStartingPosition();
	}

	private void ResetStartingPosition()
	{
		transform.position = Camera.main.ViewportToWorldPoint(startPos); // orthographic -> perspective
	}

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

		ResetStartingPosition();
		Hp = 100;
	    lastPosX = transform.position.x;
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

		GameManager.Instance.Score += (transform.position.x - this.lastPosX) * 0.25f;
	    this.lastPosX = transform.position.x;

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
