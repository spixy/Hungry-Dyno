using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obj;

    [SerializeField]
    private float minY = 0f;
    [SerializeField]
    private float maxY = 0f;

    public void Spawn(float x) {
        GameObject go = this.SpawnObject(this.obj.GetRandomItem());
        go.transform.position = new Vector3(x, Random.Range(this.minY, this.maxY), 0f);
    }

    public void ForceSpawn(float x) {
        GameObject go = PoolManager.Instantiate(this.obj.GetRandomItem());
        go.transform.position = new Vector3(x, Random.Range(this.minY, this.maxY), 0f);
    }

    public void Spawn(float minX, float maxX, float topY)
    {
        GameObject newGo = this.SpawnObject(this.obj.GetRandomItem());

        float minDistFromBorder = newGo.GetComponent<SpriteRenderer>().bounds.size.x / 2f;

        newGo.transform.position = new Vector3
        {
            x = Random.Range(minX + minDistFromBorder, maxX - minDistFromBorder),
            y = Random.Range(topY + minY, topY + maxY)
        };
    }

    private GameObject SpawnObject(GameObject go)
    {
		return GameManager.Instance.poolManager.AddToScene (go); //Instantiate(go, pos, Quaternion.identity) as GameObject;
    }
}
