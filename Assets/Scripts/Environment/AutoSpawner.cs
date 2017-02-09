using UnityEngine;
using System.Collections;

public class AutoSpawner : Spawner
{
    [SerializeField]
    private float timer = 1f;

    [SerializeField]
    private float timerDelay = 0f;

    [SerializeField]
    private bool speedup = true;

    private float factor = 1f;

    private Coroutine coroutine = null;

    public void Start()
    {
        this.coroutine = StartCoroutine(this.SpawningLoop());
    }

    public void Update() {
        if (!speedup) {
            return;
        }

        float f = 1f - (transform.position.x - 1000) / 1000;
        if (f > 1f) {
            factor = 1f;
        } else if (f > 0.4f) {  // speed up
            factor = f;
        }
    }

    private IEnumerator SpawningLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer + Random.value * (timerDelay * factor));

            if (GameManager.Instance.State == State.InGame)
				this.Spawn(this.transform.position.x);
		}
    }

    public void StopSpawning()
    {
        if (this.coroutine == null)
            return;

        StopCoroutine(this.coroutine);
        this.coroutine = null;
    }
}
