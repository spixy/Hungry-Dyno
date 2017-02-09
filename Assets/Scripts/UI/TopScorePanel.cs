using UnityEngine;
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

		var table = GameManager.Instance.scoreStorage.GetTopScoreTable(10);

		int counter = 0;

		foreach (var item in table)
		{
			player.text += ++counter + ":   " + item.Key + "\n";
			scores.text += item.Value + "\n";
		}
	}

	public void ClickBack()
	{
		this.gameObject.SetActive(false);
	}
}
