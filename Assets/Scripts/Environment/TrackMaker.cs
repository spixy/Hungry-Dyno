using UnityEngine;
using System.Collections;

public class TrackMaker : MonoBehaviour {
    public GameObject track;
    public static int count = 0;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            // TODO: don't guess numbers, add root
            Object obj = Instantiate(track, new Vector3(transform.position.x + 20, -5, 0), Quaternion.identity);
            obj.name = obj.name + count;
            count++;
        }
    }
}
