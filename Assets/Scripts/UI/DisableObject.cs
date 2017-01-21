using UnityEngine;
using System.Collections;

public class DisableObject : MonoBehaviour
{
	[SerializeField]
	private GameObject[] objectToDisable;

	[SerializeField]
	private float time;

	void OnEnable()
	{
		foreach (var go in objectToDisable)
		{
			go.SetActive(true);
		}

		StartCoroutine(DisableCoroutine());
	}

	private IEnumerator DisableCoroutine()
	{
		yield return new WaitForSeconds(time);

		foreach (var go in objectToDisable)
		{
			go.SetActive(false);
		}
	}
}
