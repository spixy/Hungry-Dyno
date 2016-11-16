using UnityEngine;
using System.Collections;

public class AttackTrigger : MonoBehaviour {
    public Collider2D attackTrigger;

    public void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Enemy")) {
            col.SendMessageUpwards("Eat");
            attackTrigger.enabled = false;
        }
    }
}
