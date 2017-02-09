using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Attack : MonoBehaviour {
    private bool attacking = false;
    private bool canAttack = true;
    private float attackTimer = 0;
    private float cdTimer = 0;
    private Animator anim;

    public Collider2D attackTrigger;

    [SerializeField]
    private const float attackDur = 0.2f;
    [SerializeField]
    private const float baseCd = 1f;
    [SerializeField]
    private const float berserkRatio = 2f;

    public float factor = 1f;

    // Can be modified with berserk
    public float attackCd = baseCd;

    private bool berserk = false;

    public void SetBerserk(bool berserk) {
        this.berserk = berserk;
        if (berserk) {
            cdTimer = 0;  // Berserk resets eat cooldown
        }
    }

    void Awake() {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

	private void Eat()
	{
		if (Debug.isDebugBuild)
			Debug.Log("Eat");

		attacking = true;
		canAttack = false;
		attackTrigger.enabled = true;
        GameManager.Instance.Dyno.Attacking = true;

        attackTimer = attackDur;
		cdTimer = attackCd;
	}

    void Update()
    {
        if (berserk) {
            attackCd = baseCd / (berserkRatio * factor);
        } else {
            attackCd = baseCd / factor;
        }

        if (canAttack && CrossPlatformInputManager.GetButtonDown("Fire1"))
	    {
		    Eat();
	    }

        if (!canAttack) {
            if (cdTimer > 0) {
                cdTimer -= Time.deltaTime;
            } else {
                canAttack = true;
            }
        }

        if (attacking) {
            if (attackTimer > 0) {
                attackTimer -= Time.deltaTime;
            } else {
                attacking = false;
                attackTrigger.enabled = false;
                GameManager.Instance.Dyno.Attacking = false;
            }
        }

        anim.SetBool("Attacking", attacking);
        GameManager.Instance.Dyno.AttackCd = (int)(((attackCd - cdTimer) / attackCd) * 100f);
    }
}
