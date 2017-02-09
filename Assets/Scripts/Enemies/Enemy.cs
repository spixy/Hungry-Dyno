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
    private bool givesBerserk = false;

    [SerializeField]
    private bool isPickup = false;

    [SerializeField]
    private bool isObstacle = false;

    private Dyno dyno;
    private Sfx sfx;

    private bool dead = false;
    private bool hit = false;

    private void Start() {
        dyno = GameManager.Instance.Dyno;
        sfx = GameManager.Instance.sfx;
	}

	private void OnEnable()
	{
		dead = false;
	}

    void OnTriggerEnter2D(Collider2D c) {
        if (isObstacle && c.gameObject.CompareTag("Player")) {
            TryDamage();
        } else if (!dead && !hit && !dyno.Attacking && c.gameObject.CompareTag("Player")) {
            TryDamage();
        }
    }

    void TryDamage() {
        if (dyno.State != DynoState.Godmode && Random.value < damageChance) {
            if (Debug.isDebugBuild)
                Debug.Log("OUCH! Damaged.");

            dyno.UpdateHP(-damage);
            hit = true;

            sfx.Slap();
        }
    }

    public void Eat()
    {
        if (hit || isObstacle) {
            return;
        } 

        // Gives as much score as he restores hp
        dyno.UpdateHP(hpRestore);
        GameManager.Instance.Score += hpRestore;

        if (givesGodmode) {
            dyno.EnableGodmode();
        }

        if (givesBerserk) {
            dyno.EnableBerserk();
        }

        dead = true;
        if (isPickup) {
            sfx.Pickup();
        } else {
            sfx.Splatter();
			GameManager.Instance.PoolManager.SpawnBlood(this.transform);
		}

		GameManager.Instance.PoolManager.RemoveFromScene(gameObject);
	}
}
