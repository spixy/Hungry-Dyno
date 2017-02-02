using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopScorePanel : MonoBehaviour
{
	[SerializeField]
	private Text player;

	[SerializeField]
	private Text scores;

	private void OnEnable()
	{
		player.text = string.Empty;
		scores.text = string.Empty;

		var table = GameManager.Instance.scoreStorage.GetTopScoreTable();

		int counter = 0;

		foreach (var item in table)
		{
			player.text += ++counter + ":   " + item.Key + "\r\n";
			scores.text += item.Value + "\r\n";
		}
	}

	public void ClickBack()
	{
		this.gameObject.SetActive(false);
	}
}
