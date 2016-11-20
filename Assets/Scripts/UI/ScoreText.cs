using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private Text textComponent;
	
	void Update()
	{
	    this.textComponent.text = "Score: " + GameManager.Instance.Score.ToString();
	}
}
