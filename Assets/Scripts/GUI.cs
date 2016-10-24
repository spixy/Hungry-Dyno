using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

    public void ButtonStartClick()
    {
        mainMenu.SetActive(false);
        GameManager.Instance.StartGame();
    }
}
