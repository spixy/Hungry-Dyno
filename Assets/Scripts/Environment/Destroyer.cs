using UnityEngine;

public class Destroyer : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
		GameManager.Instance.poolManager.RemoveFromScene(other.gameObject);
	}
}
