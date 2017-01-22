using UnityEngine;

public class Paralax : MonoBehaviour
{
	[SerializeField]
	private float speed;

	void OnEnable()
	{
		GameManager.Instance.cameraRunner.paralaxObjects.Add(this.transform);
	}

	void OnDisable()
	{
		GameManager.Instance.cameraRunner.paralaxObjects.Remove(this.transform);
	}
}
