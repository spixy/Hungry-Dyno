using UnityEngine;

public class Destroyer : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
		GameManager.Instance.PoolManager.RemoveFromScene(other.gameObject);
	}
}
