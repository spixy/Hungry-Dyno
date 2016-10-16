using UnityEngine;

/// <summary>
/// Pohyb dyna
/// </summary>
public class Dyno : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        float deltaX = 1 * Time.deltaTime * 10f;  // infinite run
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * 10f;

        this.anim.SetFloat("VerticalInput", deltaX);

        // pohyb
        this.transform.Translate(deltaX, deltaY, 0f);

        // 1 meter = 1 bod ?
        GameManager.Instance.Score = (int)this.transform.position.x;
    }
}
