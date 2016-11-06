using UnityEngine;

public class CloudMover : MonoBehaviour
{
    private float speed;

	void OnEnable()
    {
        this.speed = Random.Range(0.04f, 0.14f);
    }
	
	void Update ()
    {
	    transform.Translate(this.speed * Time.deltaTime, 0f, 0f);
    }
}
