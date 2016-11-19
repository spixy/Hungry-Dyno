using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

	public void ShowMenu()
	{
		this.mainMenu.SetActive(true);
	}

    public void HideMenu()
    {
        this.mainMenu.SetActive(false);
        GameManager.Instance.StartGame();
    }

    private void Update()
    {
        if (Input.anyKey)
        {
			this.HideMenu();
        }
    }
}
