using UnityEngine;

public class Paralax : MonoBehaviour
{
	void OnEnable()
	{
		GameManager.Instance.cameraRunner.paralaxObjects.Add(this.transform);
	}

	void OnDisable()
	{
		GameManager.Instance.cameraRunner.paralaxObjects.Remove(this.transform);
	}
}
