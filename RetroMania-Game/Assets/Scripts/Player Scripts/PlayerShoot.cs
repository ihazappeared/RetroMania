using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private WeaponManager weapon_manager;

    public float firerate = 15f;
    private float nextTimeToFire;
    public float damage;

    private bool cooldownOver;

    GameObject Temporary_Bullet_Handler;

    public GameObject[] Bullet_Emitters;
    public BulletHelper bullet_Script;
    public GameObject Bullet;

    private int currentGun;
     public float maxSpread;


    public float Bullet_Speed;

    private Camera mainCam;
    private void Awake()
    {
        weapon_manager = GetComponent<WeaponManager>();
        mainCam = Camera.main;

        cooldownOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
    }

    void WeaponShoot()
    {
        if (Input.GetMouseButtonDown(0) && cooldownOver && weapon_manager.GetCurrentSelectedWeapon().ammo > 0)
        {
            if (weapon_manager.GetCurrentSelectedWeapon().weaponType == WeaponType.BULLET)
            {
                weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();
                damage = weapon_manager.GetCurrentSelectedWeapon().damage;

                BulletFired();
            }
        }

        void BulletFired()
        {
            if(weapon_manager.GetCurrentSelectedWeapon().ammo > 0)
            {
                weapon_manager.GetCurrentSelectedWeapon().ammo -= 1;
            }

            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitters[currentGun].transform.position,
            Bullet_Emitters[currentGun].transform.rotation) as GameObject;
            Temporary_Bullet_Handler.transform.rotation = mainCam.transform.rotation;
            Temporary_Bullet_Handler.GetComponent<BulletHelper>().damage = damage;

            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            Temporary_RigidBody.AddForce(mainCam.transform.forward * Bullet_Speed);

            Destroy(Temporary_Bullet_Handler, 10.0f);
            

            cooldownOver = false;
            StartCoroutine(Coroutine(0.5f));

            currentGun = weapon_manager.GetCurrentWeapon();         
        }

        IEnumerator Coroutine(float secs)
        {
            yield return new WaitForSeconds(secs);
            cooldownOver = true;
        }
    }
}
