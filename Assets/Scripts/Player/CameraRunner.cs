using System.Collections.Generic;
using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    [SerializeField]
    private Transform player;

	private float xOffset;

	public List<Transform> paralaxObjects { get; set; }

	private float lastX;

    void Start()
    {
	    xOffset = this.transform.position.x - this.player.position.x;
		paralaxObjects = new List<Transform>();
	}

	void LateUpdate()
	{
		this.transform.position = new Vector3(this.player.position.x + xOffset, this.transform.position.y, -1f);

		int paralaxObjectsCount = paralaxObjects.Count;
		for (int i = 0; i < paralaxObjectsCount; i++)
		{
			Vector3 pos = paralaxObjects[i].position;
			pos.x += (this.transform.position.x - lastX) * 0.2f;
			paralaxObjects[i].position = pos;
		}

		lastX = this.transform.position.x;
	}
}
