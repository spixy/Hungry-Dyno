using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform parent;

    [SerializeField]
    private GameObject[] obj;

    [SerializeField]
    private float minY = 1f;

    [SerializeField]
    private float maxY = 2f; 

    public void Spawn(float x)
    {
        Vector3 pos = new Vector3(x, Random.Range(this.minY, this.maxY), 0f);
        this.SpawnObject(this.obj.GetRandomItem(), pos);
    }

    private void SpawnObject(GameObject go, Vector3 pos)
    {
        GameObject newGO = Instantiate(go, pos, Quaternion.identity) as GameObject;
        newGO.transform.SetParent(this.parent, true);
    }
}
