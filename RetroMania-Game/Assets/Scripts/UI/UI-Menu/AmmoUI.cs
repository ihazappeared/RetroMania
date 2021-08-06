using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Text ammoText;

    private WeaponManager weapon_manager;

    private GameObject Player;

    void Awake()
    {
        Player = GameObject.Find("Player");
        weapon_manager = Player.GetComponent<WeaponManager>();
    }

    void Update()
    {
        ammoText.text = weapon_manager.GetCurrentSelectedWeapon().ammo.ToString();
    }
}


