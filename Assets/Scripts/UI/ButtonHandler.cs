using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ButtonHandler : MonoBehaviour
{
	[SerializeField]
	private string Name;

#if UNITY_ANDROID
	private void Awake()
	{
		this.gameObject.SetActive(false);
	}
#endif

	public void SetDownState()
	{
		CrossPlatformInputManager.SetButtonDown(Name);
	}


	public void SetUpState()
	{
		CrossPlatformInputManager.SetButtonUp(Name);
	}
}
