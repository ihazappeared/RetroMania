using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    private int menuIndex;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuIndex = 0;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        if(menuIndex == 0)
        {
            menuIndex = 1;
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
        else
        {
            menuIndex = 0;
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }
    }
}
