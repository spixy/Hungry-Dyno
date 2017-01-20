using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField]
    private float minX = 0f;

    [SerializeField]
    private float maxX = 0f;

    private Vector3 moveVec;

	private const float MAX_DELTA_TIME = 1000f / 30f;
    
    void Awake()
    {
        this.moveVec = new Vector3(Random.Range(this.minX, this.maxX), 0f, 0f);
    }
	
	void Update()
    {
        this.transform.Translate(this.moveVec * Mathf.Min(Time.deltaTime, MAX_DELTA_TIME));
    }
}
