using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    
    void Update()
    {
        transform.position = new Vector3(player.position.x + 8, 0, -1);
    }
}
