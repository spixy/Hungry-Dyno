using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
	private int counter = 0;

	private GameObject lastGeneratedGameObject = null;
	private GameObject lastGeneratedSkyObject = null;
	private GameObject lastGeneratedTerrainObject = null;

	public enum SortingLayer
	{
		Sky,
		BackObjects,
		Terrain,
		Dyno,
		FrontObjects
	}

	void Update()
	{
		// staci to robit kazdy 5. frame
		if (counter++%5 == 0)
		{
			this.DestroyNotVisibleObjects();
			this.GenerateRandomTerrain();
		}
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
		//return; // zatial

		// vytvori teren
		this.CreateTerrainObject();

		// vytvori oblohu
		//this.CreateSkyObject();

		// vytvori objekt na pozadi
		this.CreateBasicObject(false, true);

		// vytvori koliovatelny objekt na popredi
		this.CreateBasicObject(true, Utility.GetRandomBool());
	}

	public void CreateTerrainObject()
	{
		Vector2 position;
		if (this.lastGeneratedTerrainObject == null)
		{
			position = new Vector2(0, 0);
		}
		else
		{
			Bounds lastObjectBounds = this.lastGeneratedTerrainObject.GetComponent<Renderer>().bounds;

			float x = this.lastGeneratedTerrainObject.transform.position.x + lastObjectBounds.size.x;
			float y = this.lastGeneratedTerrainObject.transform.position.y;

			position = new Vector2(x, y);
		}
		Sprite texture = this.terrainObjects.GetRandomItem();

		this.lastGeneratedTerrainObject = this.CreateGameObject("Terrain", texture, position, true, SortingLayer.Terrain);
	}

	public void CreateSkyObject()
	{
		Vector2 position;
		if (this.lastGeneratedSkyObject == null)
		{
			position = new Vector2(0, 0);
		}
		else
		{
			Bounds lastObjectBounds = this.lastGeneratedSkyObject.GetComponent<Renderer>().bounds;

			float x = this.lastGeneratedSkyObject.transform.position.x + lastObjectBounds.size.x;
			float y = this.lastGeneratedSkyObject.transform.position.y;

			position = new Vector2(x, y);
		}
		Sprite texture = this.terrainObjects.GetRandomItem();

		this.lastGeneratedSkyObject = this.CreateGameObject("Sky", texture, position, false, SortingLayer.Sky);
	}

	public void CreateBasicObject(bool isCollidable, bool isInFront)
	{
		string objectName = isCollidable ? "Collidable GameObject" : "Non collidable GameObject";
		SortingLayer sortingLayer = isInFront ? SortingLayer.FrontObjects : SortingLayer.BackObjects;
		Sprite texture = this.terrainObjects.GetRandomItem();

		Vector2 position;
		if (this.lastGeneratedGameObject == null)
		{
			position = new Vector2(0, 0);
		}
		else
		{
			Bounds lastObjectBounds = this.lastGeneratedGameObject.GetComponent<Renderer>().bounds;

			float x = this.lastGeneratedGameObject.transform.position.x + lastObjectBounds.size.x + Random.Range(1f, 1f);
			float y = this.lastGeneratedGameObject.transform.position.y;

			position = new Vector2(x, y);
		}

		this.lastGeneratedGameObject = this.CreateGameObject(objectName, texture, position, isCollidable, sortingLayer);
	}


	private GameObject CreateGameObject(string objectName, Sprite texture, Vector2 position, bool isCollidable, SortingLayer sortingLayer)
	{
		Debug.Log("Creating " + objectName + " at " + position);

		GameObject newGameObject = new GameObject(objectName);
		newGameObject.transform.SetParent(this.transform);
		newGameObject.transform.position = position;

		SpriteRenderer spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = texture;
		spriteRenderer.sortingLayerID = GetSortingLayerID(sortingLayer);

		if (isCollidable)
		{
			BoxCollider boxCollider = newGameObject.AddComponent<BoxCollider>();
			boxCollider.size = texture.bounds.size; // velkost collideru = velkost textury
		}

		this.gameObjectsInGame.Add(newGameObject);

		return newGameObject;
	}

	public static int GetSortingLayerID(SortingLayer layer)
	{
		switch (layer)
		{
			case SortingLayer.Sky:
				return UnityEngine.SortingLayer.NameToID("Sky");

			case SortingLayer.BackObjects:
				return UnityEngine.SortingLayer.NameToID("BackObjects");

			case SortingLayer.Terrain:
				return UnityEngine.SortingLayer.NameToID("Terrain");

			case SortingLayer.Dyno:
				return UnityEngine.SortingLayer.NameToID("Dyno");

			case SortingLayer.FrontObjects:
				return UnityEngine.SortingLayer.NameToID("FrontObjects");

			default:
				return -1;
		}
	}
}
