using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform parent;

    [SerializeField]
	private Spawner objectSpawner;

	[SerializeField]
	private Spawner enemySpawner;

	[SerializeField]
	private Vector2 minOrthoPos;

	[SerializeField]
	private Vector2 maxOrthoPos;

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
    [Range(0f, 1f)]
    private float objToEnemyRatio = 0.7f;
    
    private float lastPlatformX = 0f;

    private void Start()
    {
		this.GenerateStartingPlatform ();

        this.enabled = true;
    }

	public void GenerateStartingPlatform()
	{
		Platform platform = this.platforms.GetRandomItem();

		Vector3 pos = new Vector3(0f, Random.Range(minOrthoPos.y, maxOrthoPos.y), 1f);
		pos = Camera.main.ViewportToWorldPoint(pos); // orthographic -> perspective

		int length = Random.Range(2, this.lengthMax);

		this.SpawnPlatform(platform, pos, length, false);
	}

    private void Update()
    {
        if (this.lastPlatformX > this.transform.position.x)
            return;

        Platform platform = this.platforms.GetRandomItem();

        Vector3 pos = new Vector3(0f, Random.Range(minOrthoPos.y, maxOrthoPos.y), 1f);
		pos = Camera.main.ViewportToWorldPoint(pos); // orthographic -> perspective
		pos.x = this.lastPlatformX + Random.Range(gapMin, gapMax);

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
		GameObject newGO = GameManager.Instance.poolManager.AddToScene (go);
		newGO.transform.position = pos;
        newGO.transform.SetParent(parent, true);

		if (generateOtherObjects)
		{
		    this.CreateObject(pos, width);
		}
    }

    private void CreateObject(Vector3 pos, float width)
    {
        float platformWidthHalf = width / 2f;

        if (Random.value < objToEnemyRatio)
            objectSpawner.Spawn(pos.x - platformWidthHalf, pos.x + platformWidthHalf, pos.y);
        else
            enemySpawner.Spawn(pos.x - platformWidthHalf, pos.x + platformWidthHalf, pos.y);
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