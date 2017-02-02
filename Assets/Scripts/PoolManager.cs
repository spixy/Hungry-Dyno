using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Pooling objektov
/// </summary>
public class PoolManager : MonoBehaviour
{
	[SerializeField]
	private GameObject blood;

    private readonly Dictionary<string, List<GameObject>> livePool = new Dictionary<string, List<GameObject>>();

    private readonly Dictionary<string, Queue<GameObject>> deadPool = new Dictionary<string, Queue<GameObject>>(); // :)

    public void Reset()
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (var kvp in livePool)
        {
			toRemove.AddRange(kvp.Value);
        }

        for (int i = 0; i < toRemove.Count; i++)
        {
            RemoveFromScene(toRemove[i]);
        }
    }

	public GameObject SpawnBlood(Transform source)
	{
		GameObject newBlood = this.AddToScene(blood);
		newBlood.transform.position = source.position;

		return newBlood;
	}

	public void RemoveFromScene(GameObject go)
    {
        Queue<GameObject> queue;

		if (!deadPool.TryGetValue(go.name, out queue))
		{
			queue = new Queue<GameObject>();
			deadPool.Add(go.name, queue);
		}

        queue.Enqueue (go);
		go.SetActive (false);

	    List<GameObject> list;
        if (!livePool.TryGetValue(go.name, out list))
        {
            list = new List<GameObject>();
            livePool.Add(go.name, list);
        }
	    list.Remove(go);
    }

    public GameObject AddToScene(GameObject prefab, Transform parent = null)
    {
	    if (parent == null)
		    parent = this.transform;

		Queue<GameObject> queue;

		if (!deadPool.TryGetValue(prefab.name, out queue))
		{
			queue = new Queue<GameObject>();
			deadPool.Add(prefab.name, queue);
		}

	    GameObject go;

        if (queue.Count == 0)
		{
			go = this.Instantiate(prefab, parent);
		}
		else
		{
			go = queue.Dequeue ();

		    if (go.activeSelf)
            {
                go = this.Instantiate(prefab, parent);
            }

			go.SetActive (true);
		}

        List<GameObject> list;
        if (!livePool.TryGetValue(go.name, out list))
        {
            list = new List<GameObject>();
            livePool.Add(go.name, list);
        }
        list.Add(go);

	    return go;
    }

    private GameObject Instantiate(GameObject prefab, Transform parent)
    {
        GameObject go = Object.Instantiate(prefab, parent) as GameObject;
        go.name = prefab.name;
        return go;
    }
}
