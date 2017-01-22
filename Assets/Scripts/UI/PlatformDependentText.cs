using UnityEngine;
using UnityEngine.UI;

public class PlatformDependentText : MonoBehaviour
{
	[SerializeField]
	private string pcText;

	[SerializeField]
	private string androidText;

	void Awake()
	{
#if UNITY_EDITOR || UNITY_STANDALONE
		GetComponent<Text>().text = pcText;
#else
		GetComponent<Text>().text = androidText;
#endif
	}
}
