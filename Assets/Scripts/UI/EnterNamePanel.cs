using UnityEngine;
using UnityEngine.UI;

public class EnterNamePanel : MonoBehaviour
{
	[SerializeField]
	private Text scoreText;

	[SerializeField]
	private InputField inputField;

	private void OnEnable()
	{
		scoreText.text = "Your score: " + (int) GameManager.Instance.Score;

		if (!string.IsNullOrEmpty(GameManager.Instance.PlayerName))
		{
			inputField.placeholder.GetComponent<Text>().text = GameManager.Instance.PlayerName;
		}
	}

	public void ClickOK()
	{
		GameManager.Instance.PlayerName = inputField.text;
		this.gameObject.SetActive(false);
		GameManager.Instance.Gui.MainMenu.StartCloseAnimation();
	}
}
