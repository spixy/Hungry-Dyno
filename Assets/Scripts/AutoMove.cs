using UnityEngine;
using System.Collections;

public class AutoMove : MonoBehaviour
{
    [SerializeField]
    private float minX = 0f;

    [SerializeField]
    private float maxX = 0f;

    private new Transform transform;

    private Vector3 moveVec;
    
    void Awake()
    {
        this.transform = this.GetComponent<Transform>();
        this.moveVec = new Vector3(Random.Range(this.minX, this.maxX), 0f, 0f);
    }
	
	void Update()
    {
        this.transform.Translate(this.moveVec * Time.deltaTime);
    }
}
