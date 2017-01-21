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
		if (gm == null || gm.MaxScore == 0)
		{
			this.text.text = "Click to start";
		}
		else
		{
			this.text.text = "Max score: " + (int)gm.MaxScore;
		}

		animActive = false;

		up.CloseAnim();
		down.CloseAnim();
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

		up.OpenAnim();
		down.OpenAnim();
	}
}
