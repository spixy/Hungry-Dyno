using UnityEngine;
using System.Collections;

public class AutoSpawner : Spawner
{
    [SerializeField]
    private float timer = 1f;

    [SerializeField]
    private float timerDelay = 0f;

    private Coroutine coroutine = null;

    public void Start()
    {
        this.coroutine = StartCoroutine(this.SpawningLoop());
    }

    private IEnumerator SpawningLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer + Random.value * timerDelay); ;

            if (GameManager.Instance.State == State.InGame)
				base.Spawn(this.transform.position.x);
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
