using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
	private Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

	public void Add(GameObject go)
	{
		Queue<GameObject> values;

		if (!pool.TryGetValue(go.name, out values))
		{
			values = new Queue<GameObject>();
			pool.Add(go.name, values);
		}

		values.Enqueue (go);
		go.SetActive (false);
		go.transform.SetParent (this.transform);
	}

	public GameObject Get(GameObject prefab)
	{
		Queue<GameObject> values;

		if (!pool.TryGetValue(prefab.name, out values))
		{
			values = new Queue<GameObject>();
			pool.Add(prefab.name, values);
		}

		if (values.Count == 0)
		{
			GameObject go = Instantiate (prefab);
			go.name = prefab.name;
			return go;
		}
		else
		{
			GameObject go = values.Dequeue ();
			go.SetActive (true);
			return go;
		}
	}
}
