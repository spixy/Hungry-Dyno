using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public int damage = 20;

    [SerializeField]
    private float damageChance = 0.2f;

    [SerializeField]
    private int hpRestore = 20;

    [SerializeField]
    private bool givesGodmode = false;

    [SerializeField]
    private bool givesBerserk = false;

    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.CompareTag("Player")) {
            if (Random.Range(0f, 1f) < damageChance) {
                Debug.Log("OUCH! Damaged.");
                GameManager.Instance.UpdateHP(-damage);
            }
        }
    }

    public void Eat() {
        GameManager.Instance.UpdateHP(hpRestore);

        if (givesGodmode) {
            GameManager.Instance.EnableGodmode();
        }

        if (givesBerserk) {
            GameManager.Instance.EnableBerserk();
        }

        Destroy(gameObject);
    }
}
