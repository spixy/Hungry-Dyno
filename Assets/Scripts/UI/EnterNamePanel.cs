using UnityEngine;
using UnityEngine.UI;

public class EnterNamePanel : MonoBehaviour
{
	[SerializeField]
	private InputField inputField;

	private void OnEnable()
	{
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
