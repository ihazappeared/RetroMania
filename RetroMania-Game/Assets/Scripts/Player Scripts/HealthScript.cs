using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;
    private DroneController drone_Controller;

    public float health = 100f;
    public float shield = 0f;

    public bool is_Player, is_Robot, is_Drone;

    private bool is_dead;

    private LevelFinish finishScript;

    [SerializeField]
    private AudioSource PlayerHitSound;

    void Awake()
    {
        try
        {
            finishScript = GameObject.Find("--- UI ---").GetComponent<LevelFinish>();
        }
        catch (Exception e)
        {
            Debug.LogError("UI not present (LevelFinish) - Script: Health script\n" + e);
        }

        if (is_Robot)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
        }
        else if(is_Drone)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
            drone_Controller = GetComponent<DroneController>();
            navAgent = GetComponent<NavMeshAgent>();
        }
    }

    public void ApplyDamage (float damage)
    {
        float shieldDamage;
        //if dead dont execute rest of code
        if (is_dead)
            return;

        if(is_Player)
        {
            Play_HitSound();
            //display stats 
            if (shield == 0f)
            {
                health -= damage;
            }
            else
            {
                health -= damage / 2;
                shield -= damage / 2;
            }

        }

        if (is_Robot)
        {
            if(enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_distance = 50f;
            }

            health -= damage;
        }

        if(is_Drone)
        {
            health -= damage;
        }

        if(health <= 0f)
        {
            PlayerDied();

            is_dead = true;
        }
    }

    void PlayerDied()
    {
        if(is_Robot)
        {
            enemy_Anim.Dead();
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);
            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            StartCoroutine(Coroutine(3));
            finishScript.enemiesKilled += 1f;
        }

        if(is_Drone)
        {
            enemy_Anim.Dead();
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);
            drone_Controller.enabled = false;
            navAgent.enabled = false;
            StartCoroutine(Coroutine(3));
            finishScript.enemiesKilled += 1f;
        }

        IEnumerator Coroutine(int secs)
        {
            yield return new WaitForSeconds(secs);
            GetComponentInChildren<Animator>().enabled = false;
            enemy_Anim.enabled = false;
            Invoke("TurnOffGameObject", 3f);
        }

        if (is_Player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                if(enemies[i].GetComponent<EnemyController>())
                {
                    enemies[i].GetComponent<EnemyController>().enabled = false;
                }
                else
                {
                    enemies[i].GetComponent<DroneController>().enabled = false;
                }
            }

            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            GetComponent<PlayerShoot>().enabled = false;
        }

        if(tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 1f);
        }
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene005");
    }

    void TurnOffGameObject ()
    {
        gameObject.SetActive(false);
    }

    void Play_HitSound()
    {
        PlayerHitSound.Play();
    }
}
