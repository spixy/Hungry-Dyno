using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour, ISpawner
{
    [SerializeField]
    private Transform parent;

    [SerializeField]
	private Spawner objectSpawner;

	[SerializeField]
	private Spawner enemySpawner;

    [SerializeField]
    private Platform[] platforms;

    [SerializeField]
    private float gapMin = 1f;

    [SerializeField]
    private float gapMax = 2f;

    [SerializeField]
    private int lengthMin = 1;

    [SerializeField]
    private int lengthMax = 5;

    [SerializeField]
    private float minDistanceFromBorder = 0.3f;

    private float lastPlatformX = 0f;

	private List<GameObject> platformsInScene = new List<GameObject> ();

    private void Start()
    {
		GameManager.Instance.RegisterSpawner (this);

		this.GenerateStartingPlatform ();

        this.enabled = true;
    }

	private void GenerateStartingPlatform()
	{
		Platform platform = this.platforms.GetRandomItem();

		Vector3 pos = this.transform.position;
		pos.x = 0f;
		pos.z = 0f;

		int length = Random.Range(2, this.lengthMax);

		this.SpawnPlatform(platform, pos, length, false);
	}
		
	public void Reset()
	{
		foreach (GameObject go in this.platformsInScene)
			GameManager.Instance.poolManager.Add (go); //Destroy (go);

		this.platformsInScene.Clear ();

		this.GenerateStartingPlatform ();
	}

    private void Update()
    {
        if (this.lastPlatformX > this.transform.position.x)
            return;

        Platform platform = this.platforms.GetRandomItem();

        Vector3 pos = this.transform.position;
        pos.x = this.lastPlatformX + Random.Range(this.gapMin, this.gapMax);
        pos.z = 0f;

        int length = Random.Range(this.lengthMin, this.lengthMax);

        this.SpawnPlatform(platform, pos, length);
    }

	private void SpawnPlatform(Platform platform, Vector3 pos, int length, bool generateOtherObjects = true)
    {
        if (length == 1)
        {
			this.SpawnPlatform(platform.small, pos, platform.width, generateOtherObjects);
        }
        else
        {
			this.SpawnPlatform(platform.start, pos, platform.width, generateOtherObjects);

            pos.x += platform.width;

            for (int i = 2; i < length; i++)
            {
				this.SpawnPlatform(platform.mid, pos, platform.width, generateOtherObjects);

                pos.x += platform.width;
            }

			this.SpawnPlatform(platform.end, pos, platform.width, generateOtherObjects);
        }

        this.lastPlatformX = pos.x + platform.width / 2f;
    }

	private void SpawnPlatform(GameObject go, Vector3 pos, float width, bool generateOtherObjects)
    {
		GameObject newGO = GameManager.Instance.poolManager.Get (go); //Instantiate(go, pos, Quaternion.identity) as GameObject;
		newGO.transform.position = pos;
        newGO.transform.SetParent(parent, true);
		this.platformsInScene.Add (newGO);

		if (generateOtherObjects)
		{
			width /= 2f;
			float x = pos.x + Random.Range(-width + this.minDistanceFromBorder, width - this.minDistanceFromBorder);

			if (Random.value < 0.5f)
				this.objectSpawner.Spawn (x);
			else
				this.enemySpawner.Spawn (x);
		}
    }
}

[Serializable]
public struct Platform
{
    public GameObject small;

    public GameObject start;
    public GameObject mid;
    public GameObject end;

    public float width;
}