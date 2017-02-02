using System;
using UnityEngine;
using UnityEngine.UI;

public class HpText : MonoBehaviour
{
    [SerializeField]
    private Text textComponent;
	
	[SerializeField]
	private Slider sliderComponent;

	[SerializeField]
	private Image sliderBackground;

	[SerializeField]
	private int criticalHP;

	private int lastHP = 0;
	private bool lowHP = false;

	private void Update()
	{
		if (GameManager.Instance.State != State.InGame)
			return;

		int hp = (int) GameManager.Instance.Dyno.Hp;

		if (hp != lastHP)
		{
			textComponent.text = hp.ToString();
			sliderComponent.value = hp;
			lastHP = hp;
		}

		// zmena farby
		if (hp <= criticalHP)
		{
			lowHP = true;

			sliderBackground.color = Color.red;
			textComponent.color = Color.red;
		}
		else
		{
			// zresetovat farbu
			if (lowHP)
			{
				lowHP = false;

				sliderBackground.color = Color.green;
				textComponent.color = Color.white;
			}
		}
	}
}
