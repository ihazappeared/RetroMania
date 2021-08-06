using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Text healthText;

    private HealthScript healthScript;

    private GameObject Player;

    void Awake()
    {
        Player = GameObject.Find("Player");
        healthScript = Player.GetComponent<HealthScript>();
    }

    void Update()
    {
        if(healthScript.health <= 0)
        {
            healthText.text = "0" + "%";
        }
        else
        {
            healthText.text = healthScript.health.ToString() + "%";
        }
        
    }
}
