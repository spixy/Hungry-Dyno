using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ButtonHandler : MonoBehaviour
{
	[SerializeField]
	private string Name;

	public void SetDownState()
	{
		CrossPlatformInputManager.SetButtonDown(Name);
	}
}
