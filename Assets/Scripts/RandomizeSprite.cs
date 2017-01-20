using UnityEngine;

public class RandomizeSprite : MonoBehaviour
{
    [SerializeField]
    private float minScale = 0f;

    [SerializeField]
    private float maxScale = 0f;
	
    private SpriteRenderer spriteRenderer;
    
    void Awake()
    {
	    this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
	
	void OnEnable()
	{
		float scale = Random.Range(minScale, maxScale);
		this.transform.localScale = new Vector3(scale, scale, 1f);

		this.spriteRenderer.flipX = Random.value < 0.5f;
		this.spriteRenderer.flipY = Random.value < 0.5f;
	}
}
