using UnityEngine;
using System.Collections;

public class CameraRunner : MonoBehaviour {

    public Transform player;

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(player.position.x + 8, 0, -1);
    }
}
