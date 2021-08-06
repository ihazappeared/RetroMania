using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Aim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum FireType { 
    SINGLE,
    MULTIPLE
}

public enum WeaponType
{
    BULLET,
    NONE
}

public class WeaponHandler : MonoBehaviour
{
    private Animator anim;
    public Aim aim;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource shootSound;

    public FireType fireType;
    public float damage;
    public WeaponType weaponType;
    public GameObject attack_point;

    public bool hasWeapon;

    [HideInInspector]
    public float ammo;
    public float maxAmmo;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ammo = maxAmmo;
    }

    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);

    }

    void Turn_On_MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void Turn_Off_MuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void Play_ShootSound()
    {
        shootSound.Play();
    }


    public void Turn_On_AttackPoint()
    {
        attack_point.SetActive(true);
    }

    public void Turn_Off_AttackPoint()
    {
        if(attack_point.activeInHierarchy)
        {
            attack_point.SetActive(false);
        }
    }

}
