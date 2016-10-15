using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generovanie trate
/// </summary>
public class TrackManager : MonoBehaviour
{
	[SerializeField]
	private Sprite[] skies;

    [SerializeField]
    private Sprite[] terrainObjects;

    [SerializeField]
	private Sprite[] nonCollidableObjects;

    [SerializeField]
	private Sprite[] collidableObjects;

	[SerializeField]
	private Camera mainCamera;

	private List<GameObject> gameObjectsInGame = new List<GameObject>();

	public enum SortingLayer
	{
		Sky = 1,
		BackObjects = 2,
		Terrain = 3,
		Dyno = 4,
		FrontObjects = 5
	}


	void Update ()
    {
		this.DestroyNotVisibleObjects();
		this.GenerateRandomTerrain();
    }


    private void DestroyNotVisibleObjects()
    {
	    for (int i = this.gameObjectsInGame.Count - 1; i >= 0; i--)
	    {
			GameObject objectInGame = this.gameObjectsInGame[i];

			// ked je objekt uz za kamerou
			if (!objectInGame.GetComponent<Renderer>().isVisible)
			{
				Debug.Log("Removing " + objectInGame.name);

				// vymazem ho
				Destroy(objectInGame);
				this.gameObjectsInGame.RemoveAt(i);
			}
	    }
    }

	private void GenerateRandomTerrain()
	{
		return; // zatial

        // vytvori teren
		this.CreateTerrainObject(this.terrainObjects.GetRandomItem(), new Vector2(0, 0));

		// vytvori oblohu
		this.CreateSkyObject(this.terrainObjects.GetRandomItem(), new Vector2(0, 0));

        // vytvori nejake objekty
		this.CreateBasicObject(this.nonCollidableObjects.GetRandomItem(), new Vector2(0, 0), false, true);

	    bool isInFront = Random.value < 0.5f;
		this.CreateBasicObject(this.collidableObjects.GetRandomItem(), new Vector2(0, 0), true, isInFront);
    }


	public void CreateTerrainObject(Sprite texture, Vector2 position)
    {
		this.CreateGameObject("Terrain", texture, position, true, SortingLayer.Terrain);
    }

	public void CreateSkyObject(Sprite texture, Vector2 position)
	{
		this.CreateGameObject("Sky", texture, position, false, SortingLayer.Sky);
	}

	public void CreateBasicObject(Sprite texture, Vector2 position, bool isCollidable, bool isInFront)
	{
		string objectName = isCollidable ? "Collidable GameObject" : "Non collidable GameObject";
		SortingLayer sortingLayer = isInFront ? SortingLayer.FrontObjects : SortingLayer.BackObjects;

		this.CreateGameObject(objectName, texture, position, isCollidable, sortingLayer);
	}


	private void CreateGameObject(string objectName, Sprite texture, Vector2 position, bool isCollidable, SortingLayer sortingLayer)
	{
		Debug.Log("Creating " + objectName + " at " + position);

		GameObject newGameObject = new GameObject(objectName);
		newGameObject.transform.SetParent(this.transform);
		newGameObject.transform.position = position;

		SpriteRenderer spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = texture;
		spriteRenderer.sortingLayerID = (int) sortingLayer;

		if (isCollidable)
		{
			BoxCollider boxCollider = newGameObject.AddComponent<BoxCollider>();
			boxCollider.size = texture.bounds.size; // velkost collideru = velkost textury
		}

		this.gameObjectsInGame.Add(newGameObject);
    }
}
