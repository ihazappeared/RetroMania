using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCreator : MonoBehaviour
{

    public GameObject[] Bullet_Emitters;
    private BulletHelper bullet_Script;
    GameObject Temporary_Bullet_Handler;
    GameObject Temporary_Bullet_Handler1;
    public GameObject Bullet;

    public float Bullet_Speed;

    public float damage;

    private Transform target;

    public bool shoot;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    void Update()
    {
        damage = GameObject.Find("Player").GetComponent<WeaponManager>().GetCurrentSelectedWeapon().damage;

        if(shoot)
        {
            BulletFired();
        }
    }

    void BulletFired()
    {
        Quaternion rot = Quaternion.LookRotation(target.transform.position - transform.position);
        //First Bullet
        Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitters[0].transform.position, rot) as GameObject;

        Temporary_Bullet_Handler.transform.rotation = rot;
        Temporary_Bullet_Handler.GetComponent<BulletHelper>().damage = damage;
        Temporary_Bullet_Handler.GetComponent<BulletHelper>().shotBy = 1;

        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

        Temporary_RigidBody.AddForce(transform.forward * Bullet_Speed);
        Debug.Log("Created First bullet");

        Destroy(Temporary_Bullet_Handler, 10.0f);

        

        //Second bullet
        Temporary_Bullet_Handler1 = Instantiate(Bullet, Bullet_Emitters[1].transform.position, rot) as GameObject;

        Temporary_Bullet_Handler1.transform.rotation = rot;
        Temporary_Bullet_Handler1.GetComponent<BulletHelper>().damage = damage;
        Temporary_Bullet_Handler1.GetComponent<BulletHelper>().shotBy = 1;

        Rigidbody Temporary_RigidBody1;
        Temporary_RigidBody1 = Temporary_Bullet_Handler1.GetComponent<Rigidbody>();

        Temporary_RigidBody1.AddForce(transform.forward * Bullet_Speed);
        Debug.Log("Created Second bullet");

        Destroy(Temporary_Bullet_Handler1, 10.0f);

        shoot = false;
    }
}
