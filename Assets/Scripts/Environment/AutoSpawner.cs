using UnityEngine;
using System.Collections;

public class AutoSpawner : Spawner
{
    [SerializeField]
    private float timer = 0f;

    private Coroutine coroutine = null;

	private void Start()
	{
		GameManager.Instance.RegisterSpawner (this);
	}

    public void StartSpawning()
    {
        if (this.coroutine != null)
            return;

        this.coroutine = StartCoroutine(this.SpawningLoop());
    }

    private IEnumerator SpawningLoop()
    {
        WaitForSeconds wfs = new WaitForSeconds(this.timer);

        while (true)
        {
            yield return wfs;

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
