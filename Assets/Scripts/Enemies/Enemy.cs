using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int hpRestore = 20;

    [SerializeField]
    private int damage = 20;

    [SerializeField]
    private float damageChance = 0.2f;

    [SerializeField]
    private bool givesGodmode = false;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private bool givesBerserk = false;

    [SerializeField]
    private bool isPickup = false;

    private Dyno dyno;
    private Sfx sfx;

    private bool dead = false;

	private void Start() {
        dyno = GameManager.Instance.dyno;
        sfx = GameManager.Instance.sfx;
    }

    void OnTriggerEnter2D(Collider2D c) {
        if (!dead && !dyno.Attacking && c.gameObject.CompareTag("Player")) {
            if (dyno.State != DynoState.Godmode && Random.value < damageChance) {
				if (Debug.isDebugBuild)
					Debug.Log("OUCH! Damaged.");

                dyno.UpdateHP(-damage);

                sfx.Slap();
            }
        }
    }

    public void Eat()
    {
        // Gives as much score as he restores hp
        dyno.UpdateHP(hpRestore);
        GameManager.Instance.Score += hpRestore;

        if (givesGodmode) {
            dyno.EnableGodmode();
        }

        if (givesBerserk) {
            dyno.EnableBerserk();
        }

        //Destroy(gameObject);
        dead = true;
        if (isPickup) {
            sfx.Pickup();
            GameManager.Instance.poolManager.RemoveFrowScene(gameObject);
        } else {
            sfx.Splatter();
            if (animator != null) {
                animator.SetBool("Dead", true);
            }
        }
    }
}
