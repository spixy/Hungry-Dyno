using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private Camera cam;

    void Awake()
    {
        this.cam = this.GetComponent<Camera>();
    }

    void LateUpdate()
    {
        this.cam.orthographicSize = 10f / Screen.width * Screen.height;

        this.transform.position = new Vector3(this.player.position.x + 8.5f, 0f, -1f);
    }
}
