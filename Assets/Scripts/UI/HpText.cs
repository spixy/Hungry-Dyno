using UnityEngine;
using UnityEngine.UI;

public class HpText : MonoBehaviour
{
    [SerializeField]
    private Text textComponent;

	[SerializeField]
	private int criticalHP;

	[SerializeField]
	private float blickSpeed;

	private int lastHP = 0;
	private float timer;
	private bool blicking = false;

	private void Update()
	{
		if (GameManager.Instance.State != State.InGame)
			return;

	    int hp = (int) GameManager.Instance.dyno.Hp;

	    if (hp != lastHP)
	    {
		    textComponent.text = "HP: " + hp;
		    lastHP = hp;
	    }

		// blikanie biela - cervena
		if (hp <= criticalHP)
		{
			blicking = true;
			timer -= Time.deltaTime;

			if (timer <= 0)
			{
				Color c = textComponent.color;
				c.g = 1f - c.g;
				c.b = 1f - c.b;
				textComponent.color = c;

				timer = blickSpeed;
			}
		}
		else
		{
			// zresetovat farbu na bielu
			if (blicking)
			{
				blicking = false;
				textComponent.color = Color.white;
				timer = blickSpeed;
			}
		}
    }
}
