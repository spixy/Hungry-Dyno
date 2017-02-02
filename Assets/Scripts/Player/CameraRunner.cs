using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    [SerializeField]
    private Transform player;

	[SerializeField]
	private float speedRatio;

	private float xOffset;
	private float lastX;

    void Start()
    {
	    xOffset = this.transform.position.x - this.player.position.x;
	    lastX = player.position.x;
    }

	void LateUpdate()
	{
		if (this.player.position.x <= lastX)
			return;

		this.transform.position = new Vector3(this.player.position.x + xOffset, this.transform.position.y, -1f);

		xOffset += (speedRatio * Time.deltaTime);
		lastX = this.player.position.x;
	}
}
