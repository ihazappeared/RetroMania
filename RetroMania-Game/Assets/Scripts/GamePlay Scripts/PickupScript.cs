using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public bool isAmmo;
    public bool isHealth;
    public bool isShield;
    public bool ispickupGun;

    private LevelFinish finishScript;
    private Camera mainCam;

    public RaycastHit hit;

    PickupSoundScript sound;
    HealthScript healthScript;
    WeaponManager weaponManager;

    void Start()
    {
        healthScript = GameObject.Find(Tags.PLAYER_TAG).GetComponent<HealthScript>();
        weaponManager = GameObject.Find(Tags.PLAYER_TAG).GetComponent<WeaponManager>();

        try
        {
            finishScript = GameObject.Find("--- UI ---").GetComponent<LevelFinish>();
        }
        catch (Exception e)
        {
            Debug.LogError("UI not present (LevelFinish) - Script: PickupScript\n" + e);
        }

        sound = GameObject.Find("AudioManager").GetComponentInChildren<PickupSoundScript>();
        mainCam = Camera.main;

        if (this.gameObject.name == "GunPickup")
        {
            ispickupGun = true;
        }
        else
        {
            ispickupGun = false;
        }
    }

    void Update()
    {
        if(ispickupGun)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, 3))
                {
                    if(hit.transform.gameObject == this.gameObject)
                    {
                        Debug.Log("Gunpickedup");
                    }
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAmmo || isHealth || isShield)
        {
            if (other.gameObject.tag == Tags.PLAYER_TAG)
            {
                if (isAmmo)
                {
                    weaponManager.GetCurrentSelectedWeapon().ammo += 10f;
                    finishScript.pickupsGained += 1f;
                }
                else if (isHealth)
                {
                    healthScript.health += 5f;
                    finishScript.pickupsGained += 1f;
                }
                else if (isShield)
                {
                    healthScript.shield += 10f;
                    finishScript.pickupsGained += 1f;
                }

                this.gameObject.SetActive(false);
                sound.PlayPickUpSound();
            }
        }
    }
}
