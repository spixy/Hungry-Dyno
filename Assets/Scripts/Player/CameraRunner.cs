using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private Camera cam;

    void Awake()
    {
        this.cam = this.GetComponent<Camera>();

		this.cam.orthographicSize = 10f / Screen.width * Screen.height;

		this.transform.position = new Vector3(this.player.position.x + 8.5f, this.player.position.y + 3.5f, -1f);
    }

	void LateUpdate()
	{
		this.cam.orthographicSize = 10f / Screen.width * Screen.height;

		this.transform.position = new Vector3(this.player.position.x + 8.5f, this.transform.position.y, -1f);
	}
}
