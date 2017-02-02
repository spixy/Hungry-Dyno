using UnityEngine;

public class Teeth : MonoBehaviour
{
	[SerializeField]
	private Animator animator;

	public void OnCompleteAnim()
	{
		GameManager.Instance.Gui.MainMenu.OnCompleteAnimation();
	}

	public void CloseAnim()
	{
		animator.ResetTrigger("Play");
		animator.SetTrigger("Reset");
	}

	public void OpenAnim()
	{
		animator.ResetTrigger("Reset");
		animator.SetTrigger("Play");
	}
}
