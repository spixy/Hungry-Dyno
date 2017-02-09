using UnityEngine;

public class BgRunner : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float speedRatio;

    [SerializeField]
    private bool stopOnReverse;
    
    private float lastX;

    void Start() {
        lastX = player.position.x;
    }

    void LateUpdate() {
        if (stopOnReverse && player.position.x <= lastX)
            return;

        this.transform.position = new Vector3(this.player.position.x * speedRatio, this.transform.position.y, -1f);
        lastX = this.player.position.x;
    }
}
