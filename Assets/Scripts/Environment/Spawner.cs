using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour, ISpawner
{
    [SerializeField]
    private Transform parent;

    [SerializeField]
    private GameObject[] obj;

    [SerializeField]
    private float minY = 1f;

    [SerializeField]
    private float maxY = 2f; 

	private List<GameObject> objectsInScene = new List<GameObject> ();

	private void Start()
	{
		GameManager.Instance.RegisterSpawner (this);
	}

	public void Reset()
	{
		foreach (GameObject go in this.objectsInScene)
			Destroy (go);

		this.objectsInScene.Clear ();
	}

    public void Spawn(float x)
    {
        Vector3 pos = new Vector3(x, Random.Range(this.minY, this.maxY), 0f);
        this.SpawnObject(this.obj.GetRandomItem(), pos);
    }

    private void SpawnObject(GameObject go, Vector3 pos)
    {
        GameObject newGO = Instantiate(go, pos, Quaternion.identity) as GameObject;
        newGO.transform.SetParent(this.parent, true);
		this.objectsInScene.Add (newGO);
    }
}
