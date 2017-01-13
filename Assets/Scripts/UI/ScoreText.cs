using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private Text textComponent;

	private int lastScore = 0;

	void Update()
	{
		if (GameManager.Instance.State != State.InGame)
			return;

		int score = (int) GameManager.Instance.Score;

		if (score != lastScore)
		{
			textComponent.text = "Score: " + score;
			lastScore = score;
		}
	}
}
