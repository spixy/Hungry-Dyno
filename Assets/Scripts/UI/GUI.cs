using UnityEngine;

public class GUI : MonoBehaviour
{
    [SerializeField]
    private MainMenu mainMenu;

	[SerializeField]
	private GameMenu gameMenu;

	private const float freeze = 0.3f;

	private float showTime = 0f;

	public MainMenu MainMenu
	{
		get { return mainMenu; }
	}

	public GameMenu GameMenu
	{
		get { return gameMenu; }
	}

	private void OnEnable()
	{
		ShowMenu();
	}

	public void ShowMenu()
	{
		this.mainMenu.gameObject.SetActive(true);
		this.gameMenu.gameObject.SetActive(false);
		showTime = Time.unscaledTime;
	}

    public void HideMenu()
    {
	    if (Time.unscaledTime - showTime < freeze)
		    return;

        this.mainMenu.gameObject.SetActive(false);
		this.gameMenu.gameObject.SetActive(true);
		GameManager.Instance.StartGame();
    }
}
