using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    [SerializeField]
    private Transform player;

	private float xOffset;

    void Start()
    {
	    xOffset = this.transform.position.x - this.player.position.x;
    }

	void LateUpdate()
	{
		this.transform.position = new Vector3(this.player.position.x + xOffset, this.transform.position.y, -1f);
	}
}
