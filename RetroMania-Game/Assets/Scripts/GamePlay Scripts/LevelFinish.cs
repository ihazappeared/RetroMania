using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject Pause;
    public GameObject PlayerUI;

    public bool active;

    public Text killedIntialText;
    public Text killedFinalText;

    public Text pickupsIntialText;
    public Text pickupsFinalText;

    public Text totalText;

    private float total;

    [HideInInspector]
    public float enemiesKilled;
    [HideInInspector]
    public float pickupsGained;

    // Start is called before the first frame update
    void Start()
    {
        WinScreen.SetActive(false);

        DisableAllText();
    }

    void DisableAllText()
    {
        killedIntialText.text = "";
        killedIntialText.text = "";

        pickupsIntialText.text = "";
        pickupsFinalText.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {

            GameObject.Find("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerShoot>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            WinScreen.SetActive(true);
            this.gameObject.GetComponent<PauseMenu>().enabled = false;
            GameObject.Find("PlayerUI").SetActive(false);
            SetEnemiesKilled();
        }
    }

    void SetEnemiesKilled()
    {
        active = false;
        killedIntialText.text = "Enemies killed: ";
        killedFinalText.text = enemiesKilled + "x50p = " + (enemiesKilled * 50);
        total += (enemiesKilled * 50);
        SetPickupsGained();
    }

    void SetPickupsGained()
    {
        pickupsIntialText.text = "Pickups: ";
        pickupsFinalText.text = pickupsGained + "x25p = " + (pickupsGained * 25);
        total += (pickupsGained * 25);
        totalText.text = "Total points: " + total;
       
    }



    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene004");
    }
}
