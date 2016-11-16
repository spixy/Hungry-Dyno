using UnityEngine;
using System.Collections;

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

    // Can be modified with berserk
    public float attackCd = baseCd;

    public void SetBerserk(bool berserk) {
        if (berserk) {
            attackCd /= berserkRatio;
        } else {
            attackCd = baseCd;
        }
    }

    void Awake() {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

    void Update() {
        if (Input.GetKeyDown("a") && canAttack) {
            attacking = true;
            canAttack = false;
            attackTrigger.enabled = true;

            attackTimer = attackDur;
            cdTimer = attackCd;
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
            }
        }

        anim.SetBool("Attacking", attacking);
	}
}
