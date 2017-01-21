using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Pooling objektov
/// </summary>
public class PoolManager : MonoBehaviour
{
    private readonly Dictionary<string, List<GameObject>> livePool = new Dictionary<string, List<GameObject>>();

    private readonly Dictionary<string, Queue<GameObject>> deadPool = new Dictionary<string, Queue<GameObject>>(); // :)

    public void Reset()
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (var kvp in livePool)
        {
            foreach (GameObject go in kvp.Value)
            {
				if (go)
					toRemove.Add(go);
            }
        }

        for (int i = 0; i < toRemove.Count; i++)
        {
            RemoveFrowScene(toRemove[i]);
        }
    }

	public void RemoveFrowScene(GameObject go)
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

    public GameObject AddToScene(GameObject prefab)
    {
        Queue<GameObject> queue;

		if (!deadPool.TryGetValue(prefab.name, out queue))
		{
			queue = new Queue<GameObject>();
			deadPool.Add(prefab.name, queue);
		}

	    GameObject go;

        if (queue.Count == 0)
		{
			go = Instantiate(prefab);
		}
		else
		{
			go = queue.Dequeue ();

		    if (go.activeSelf)
            {
                go = Instantiate(prefab);
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

    private static GameObject Instantiate(GameObject prefab)
    {
        GameObject go = Object.Instantiate(prefab);
        go.name = prefab.name;
        return go;
    }
}
