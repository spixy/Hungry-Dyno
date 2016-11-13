using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform parent;

    [SerializeField]
    private Spawner objectSpawner;

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

    private float lastPlatformX = 0f;

    private void Start()
    {
        Platform platform = this.platforms.GetRandomItem();

        Vector3 pos = this.transform.position;
        pos.x = 0f;

        int length = Random.Range(2, this.lengthMax);

        this.SpawnPlatform(platform, pos, length);

        this.enabled = true;
    }

    private void Update()
    {
        if (this.lastPlatformX > this.transform.position.x)
            return;

        Platform platform = this.platforms.GetRandomItem();

        Vector3 pos = this.transform.position;
        pos.x = this.lastPlatformX + Random.Range(this.gapMin, this.gapMax);

        int length = Random.Range(this.lengthMin, this.lengthMax);

        this.SpawnPlatform(platform, pos, length);
    }

    private void SpawnPlatform(Platform platform, Vector3 pos, int length)
    {
        if (length == 1)
        {
            this.SpawnPlatform(platform.small, pos, platform.width);
        }
        else
        {
            this.SpawnPlatform(platform.start, pos, platform.width);

            pos.x += platform.width;

            for (int i = 2; i < length; i++)
            {
                this.SpawnPlatform(platform.mid, pos, platform.width);

                pos.x += platform.width;
            }

            this.SpawnPlatform(platform.end, pos, platform.width);
        }

        this.lastPlatformX = pos.x + platform.width / 2f;
    }

    private void SpawnPlatform(GameObject go, Vector3 pos, float width)
    {
        GameObject newGO = Instantiate(go, pos, Quaternion.identity) as GameObject;
        newGO.transform.SetParent(parent, true);

        float x = Random.Range(0.2f, width - 0.2f);
        this.objectSpawner.Spawn(pos.x + x);
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