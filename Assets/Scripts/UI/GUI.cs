using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

    public void ButtonStartClick()
    {
        this.mainMenu.SetActive(false);
        GameManager.Instance.StartGame();
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            this.ButtonStartClick();
        }
    }
}
