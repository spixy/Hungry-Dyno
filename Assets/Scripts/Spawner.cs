using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject[] obj;
    public float spawnMin = 1f;
    public float spawnMax = 2f;

    // Use this for initialization
    void Start() {
        Spawn();
    }

    void Spawn() {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - Random.Range(0f, 1f), 0);
        Instantiate(obj[Random.Range(0, obj.Length)], pos, Quaternion.identity);
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }
}
