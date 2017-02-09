using UnityEngine;
using System.Collections;

public class ObstacleSpawner : Spawner {
    [SerializeField]
    private float timer = 4f;

    [SerializeField]
    private float refresh = 0.1f;

    private Coroutine coroutine = null;

    private Vector2 BOX = new Vector2(8f, 2f);

    public void Start() {
        this.coroutine = StartCoroutine(this.SpawningLoop());
    }

    private IEnumerator SpawningLoop() {
        while (true) {
            Vector2 center = new Vector2(transform.position.x, transform.position.y);
            if (Physics2D.OverlapBox(center, BOX, 0) != null) {
                yield return new WaitForSeconds(refresh);
            } else {
                if (GameManager.Instance.State == State.InGame) {
                    this.Spawn(this.transform.position.x);
                }

                yield return new WaitForSeconds(timer);
            }
        }
    }

    public void StopSpawning() {
        if (this.coroutine == null)
            return;

        StopCoroutine(this.coroutine);
        this.coroutine = null;
    }
}
