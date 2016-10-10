using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generovanie trate
/// </summary>
public class TrackManager : MonoBehaviour
{
    [SerializeField]
    private Texture[] groundTextures;

    [SerializeField]
    private Texture[] nonCollidableObjects;

    [SerializeField]
    private Texture[] collidableObjects;

    private List<GameObject> gameObjectsInGame = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        // this.GenerateRandomTerrain();
	    this.DestroyOldObjects();
    }

    private void DestroyOldObjects()
    {
        /*foreach (GameObject gameObject in this.gameObjectsInGame)
        {
            if (gameObject not in scene)
            {
                Destroy(gameObject);
            }
        }*/
    }

    private void GenerateRandomTerrain()
    {
        // vytvori teren
        Texture randomTexture = GetRandomItem(this.groundTextures);
        this.CreateTerrainGameObject(randomTexture, new Vector2(0, 0));

        // vytvori nejake objekty
        this.CreateNonCollidableGameObject(GetRandomItem(this.nonCollidableObjects), new Vector2(0, 0));
        this.CreateCollidableGameObject(GetRandomItem(this.collidableObjects), new Vector2(0, 0));
    }

    private void CreateTerrainGameObject(Texture texture, Vector2 position)
    {
        GameObject newGameObject = new GameObject("TerrainGameObject");
        // add texture
        // set position
        // set layer
        this.gameObjectsInGame.Add(newGameObject);
    }

    private void CreateNonCollidableGameObject(Texture texture, Vector2 position)
    {
        GameObject newGameObject = new GameObject("NonCollidableGameObject");
        // add texture
        // set position
        // set layer
        this.gameObjectsInGame.Add(newGameObject);
    }

    private void CreateCollidableGameObject(Texture texture, Vector2 position)
    {
        GameObject newGameObject = new GameObject("CollidableGameObject");
        // add texture
        // set position
        // set layer
        this.gameObjectsInGame.Add(newGameObject);
    }

    private static T GetRandomItem<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}
