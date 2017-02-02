using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

	[SerializeField]
	private GameObject enterNamePanel;

	private bool animActive;
	private bool firstStart = true;

	private void OnEnable()
	{
		if (firstStart)
		{
			StartCloseAnimation();
			firstStart = false;
		}
		else
		{
			enterNamePanel.SetActive(true);
		}
	}

	public void StartCloseAnimation()
	{
		if (!firstStart)
		{
			this.text.text = "Score: " + (int)GameManager.Instance.Score + "\r\nMax score: " + GameManager.Instance.MaxScore;
		}

		animActive = false;
		clickText.enabled = true;

		StartCoroutine(PositionChangeLoop());

		up.CloseAnim();
		down.CloseAnim();
	}

	public void OnCompleteAnimation()
	{
		if (animActive)
		{
			animActive = false;
			GameManager.Instance.Gui.HideMenu();
		}
	}

	public void StartOpenAnimation()
	{
		if (animActive)
			return;

		animActive = true;
		clickText.enabled = false;

		up.OpenAnim();
		down.OpenAnim();
	}

	private IEnumerator PositionChangeLoop()
	{
		while (clickText.enabled)
		{
			(clickText.transform as RectTransform).anchoredPosition = new Vector2(Random.Range(-450, 450), Random.Range(-300,300));
			yield return new WaitForSecondsRealtime(3f);
		}
	}
}
