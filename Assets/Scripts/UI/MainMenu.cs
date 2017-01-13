using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private Text text;

	private void Start()
	{
		GameManager.Instance.touchController.uiRectTransforms.Add(this.transform as RectTransform);
	}

	private void OnEnable()
	{
		GameManager gm = GameManager.Instance;

		if (gm == null)
			return;

		this.text.text = "Click to start";

		if (gm.MaxScore > 0) {
			this.text.text += "\r\n\r\nMax score: " + (int)gm.MaxScore;
		}
	}
}
