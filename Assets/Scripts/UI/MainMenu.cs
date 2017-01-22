using System.Collections;
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

	[SerializeField]
	private Text clickText;

	private bool animActive;

	private void OnEnable()
	{
		GameManager gm = GameManager.Instance;
		if (gm != null || gm.MaxScore > 0)
		{
			this.text.text = "Max score: " + (int)gm.MaxScore;
		}

		animActive = false;
		clickText.enabled = true;

		StartCoroutine(Loop());

		up.CloseAnim();
		down.CloseAnim();
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
		clickText.enabled = false;

		up.OpenAnim();
		down.OpenAnim();
	}

	private IEnumerator Loop()
	{
		while (clickText.enabled)
		{
			(clickText.transform as RectTransform).anchoredPosition = new Vector2(Random.Range(-450, 450), Random.Range(-300,300));
			yield return new WaitForSecondsRealtime(3f);
		}
	}
}
