using UnityEngine;

public class GUI : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

	[SerializeField]
	private GameObject gameMenu;

	private const float freeze = 0.3f;

	private float showTime = 0f;

	public void ShowMenu()
	{
		this.mainMenu.SetActive(true);
		this.gameMenu.SetActive(false);
		showTime = Time.unscaledTime;
	}

    public void HideMenu()
    {
	    if (Time.unscaledTime - showTime < freeze)
		    return;

        this.mainMenu.SetActive(false);
		this.gameMenu.SetActive(true);
		GameManager.Instance.StartGame();
    }

    private void Update()
    {
        if (GameManager.Instance.State == State.MainMenu && Input.anyKey && this.mainMenu.activeInHierarchy)
        {
			this.HideMenu();
        }
    }
}
