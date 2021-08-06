using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    public Text shieldText;

    private HealthScript healthScript;

    private GameObject Player;

    void Awake()
    {
        Player = GameObject.Find("Player");
        healthScript = Player.GetComponent<HealthScript>();
    }

    void Update()
    {
        shieldText.text = healthScript.shield.ToString() + "%";
    }
}
