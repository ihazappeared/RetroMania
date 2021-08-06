using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelper : MonoBehaviour
{
    public GameObject attack_point;
    private EnemyAnimator enemy_Anim;
    public bool is_Robot;
    private bool attacking = false;
    public float animLength = 1.5f;

    [SerializeField]
    private GameObject Explosion;

    void Awake()
    {
        attack_point = this.transform.GetChild(0).gameObject;
        if (is_Robot)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
        }
    }

    void Update()
    {

    }

    public void Turn_On_AttackPoint()
    {
        attack_point.SetActive(true);
    }

    public void Turn_Off_AttackPoint()
    {
        if (attack_point.activeInHierarchy)
        {
            attack_point.SetActive(false);
        }
    }

    public void PlayExplosion()
    {
        Explosion.SetActive(true);
    }

    public void StopExplosion()
    {
        Explosion.SetActive(false);
    }
}
