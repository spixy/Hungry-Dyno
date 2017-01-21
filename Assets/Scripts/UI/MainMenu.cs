using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private Text text;

	[SerializeField]
	private Teeth up;

	[SerializeField]
	private Teeth down;

	private bool animActive;

	private void OnEnable()
	{
		GameManager gm = GameManager.Instance;

		this.text.text = "Click to start";

		if (gm.MaxScore > 0)
		{
			this.text.text += "\r\n\r\nMax score: " + (int)gm.MaxScore;
		}

		animActive = false;
		up.Animator.ResetTrigger("Play");
		down.Animator.ResetTrigger("Play");
	}

	private void Update()
	{
		if (!animActive && Input.anyKey)
		{
			StartMenuAnimation();
		}
	}

	public void OnCompleteAnimation()
	{
		if (animActive)
		{
			animActive = false;
			GameManager.Instance.gui.HideMenu();
		}
	}

	public void StartMenuAnimation()
	{
		if (animActive)
			return;

		animActive = true;

		up.Animator.SetTrigger("Play");
		down.Animator.SetTrigger("Play");
	}
}
