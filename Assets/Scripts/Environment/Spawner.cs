using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public Transform environment;
    public GameObject[] obj;
    public float spawnMin = 1f;
    public float spawnMax = 2f;

    [SerializeField]
    private float verticality = 1f;
    
    void Start()
    {
        StartCoroutine(this.SpawningCoroutine());
    }

    private IEnumerator SpawningCoroutine()
    {
        GameManager gameManager = GameManager.Instance;

        while (true)
        {
            if (gameManager.InGame)
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y - Random.Range(0f, verticality), 0);

                GameObject newObject = Instantiate(obj.GetRandomItem(), pos, Quaternion.identity) as GameObject;
                newObject.transform.SetParent(environment);
            }

            yield return new WaitForSeconds(Random.Range(spawnMin, spawnMax));
        }
    }
}
