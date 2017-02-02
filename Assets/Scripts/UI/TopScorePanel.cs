using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopScorePanel : MonoBehaviour
{
	[SerializeField]
	private Text inputField;

	private void OnEnable()
	{
		inputField.text = string.Empty;

		var table = GameManager.Instance.scoreStorage.GetTopScoreTable();

		int counter = 0;

		foreach (var item in table)
		{
			inputField.text += "Player " + ++counter +": " + item.Key + ", Score: " + item.Value + "\r\n";
		}
	}

	public void ClickBack()
	{
		this.gameObject.SetActive(false);
	}
}
