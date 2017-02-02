using UnityEngine;
using System.Collections;

public class RandomYellow : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private IEnumerator ColorLoop()
	{
		yield return new WaitForSeconds(0.1f);

		Color c = spriteRenderer.color;
		c.b = Mathf.Clamp(c.b + Random.value - 0.5f, 40f, 120f);

		spriteRenderer.color = c;
	}
}
