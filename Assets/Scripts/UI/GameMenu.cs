using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
	[SerializeField]
	private Image icon;

	[SerializeField]
	private Sprite hearthIcon;

	[SerializeField]
	private Sprite radioActiveIcon;

	[SerializeField]
	private Sprite skullIcon;

	[SerializeField]
	private Sprite starIcon;

	public void SetIcon(DynoState dynoState)
	{
		switch (dynoState)
		{
			case DynoState.Death:
				icon.sprite = skullIcon;
				break;

			case DynoState.Normal:
				icon.sprite = hearthIcon;
				break;

			case DynoState.Godmode:
				icon.sprite = starIcon;
				break;

			case DynoState.Berserk:
				icon.sprite = radioActiveIcon;
				break;
		}
	}
}
