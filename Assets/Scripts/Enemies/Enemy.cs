using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
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

    void OnTriggerEnter2D(Collider2D c) {
        if (!GameManager.Instance.dyno.Attacking && c.gameObject.CompareTag("Player")) {
            if (!GameManager.Instance.dyno.Godmode && Random.value < damageChance) {
                Debug.Log("OUCH! Damaged.");
                GameManager.Instance.dyno.UpdateHP(-damage);
            }
        }
    }

    public void Eat()
    {
        // Gives as much score as he restores hp
        GameManager.Instance.dyno.UpdateHP(hpRestore);
        GameManager.Instance.Score += hpRestore;

        if (givesGodmode) {
            GameManager.Instance.dyno.EnableGodmode();
        }

        if (givesBerserk) {
            GameManager.Instance.dyno.EnableBerserk();
        }

        //Destroy(gameObject);
        GameManager.Instance.poolManager.Add(gameObject);
    }
}
