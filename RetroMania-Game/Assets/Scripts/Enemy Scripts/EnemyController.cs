using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK,
    SHOOT
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;

    private EnemyState enemy_State;

    public float walk_speed = 0.5f;

    public float chase_distance = 7f;
    private float current_Chase_Distance;
    public float attack_distance = 1.8f;
    public float shoot_distance = 40f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    public float cooldown;
    private bool cooldownOver = true;

    public bool isDrone;

    private Transform target;

    RaycastHit vRaycast;

    void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy_State = EnemyState.PATROL;

        patrol_Timer = patrol_For_This_Time;

        attack_Timer = wait_Before_Attack;

        //memorize value of distance
        current_Chase_Distance = chase_distance;

    }

    // Update is called once per frame
    void Update()
    {
        //For debug
        if (Physics.Raycast(navAgent.transform.position, (target.transform.position - navAgent.transform.position), out vRaycast))
        {
            if (vRaycast.transform == target)
            {
                Debug.DrawRay(navAgent.transform.position, (target.transform.position - navAgent.transform.position), Color.green);
            }
            else
            {
                Debug.DrawRay(navAgent.transform.position, (target.transform.position - navAgent.transform.position), Color.red);
            }
        }
        


        //Determines which function to use based on state
        if (enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }

        if(enemy_State == EnemyState.CHASE)
        {
            Chase();   
        }

        if (enemy_State == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    void Patrol()
    {
        //tell navAgent that enemy can move
        navAgent.isStopped = false;
        navAgent.speed = walk_speed;

        RaycastHit vRaycast;

        patrol_Timer += Time.deltaTime;

        if(patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();
            patrol_Timer = 0f;
        }

        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Walk(true);
        }
        else
        {
            enemy_Anim.Walk(false);
        }

        //distance between player and enemy
        //if raycast hits player (if player is seen) chase
        if(Vector3.Distance(transform.position, target.position) <= chase_distance && 
            Physics.Raycast(navAgent.transform.position, (target.transform.position - navAgent.transform.position), out vRaycast))
        {
            if (vRaycast.transform == target)
            {
                enemy_State = EnemyState.CHASE;
            }
            //play spotted audio
        }
    }

    void Chase()
    {
        navAgent.isStopped = false;

        //set player position as target destination
        navAgent.SetDestination(target.position);


        if (Vector3.Distance(transform.position, target.position) <= attack_distance && !isDrone)
        {
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            //reset chase distance to previous value
            if(chase_distance != current_Chase_Distance)
            {
                chase_distance = current_Chase_Distance;
            }
        }
        else if (Vector3.Distance(transform.position, target.position) > chase_distance)
        {
            //stop running
            enemy_State = EnemyState.PATROL;
            patrol_Timer = patrol_For_This_Time;

            if(chase_distance != current_Chase_Distance)
            {
                chase_distance = current_Chase_Distance;
            }
        }
    }

    void Attack()
    {
        //Look at Player
        Vector3 rot = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
        rot.x = rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);

        //Stop Enemy movement
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;

        if (attack_Timer > wait_Before_Attack)
        {
            //play attack animation
            enemy_Anim.Attack();

            attack_Timer = 0f;
        }

        //If target isnt in attack distance chase him
        if(Vector3.Distance(transform.position, target.position) > attack_distance + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
        }
    }

    void Shoot()
    {
        //Look at Player
        Vector3 rot = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
        rot.x = rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);

        //Stop Enemy movement
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;

        if (attack_Timer > wait_Before_Attack)
        {
            //play attack animation
            enemy_Anim.Attack();

            attack_Timer = 0f;
        }

        cooldownOver = false;

        //If target isnt in attack distance chase him
        if (Vector3.Distance(transform.position, target.position) > shoot_distance + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
            StartCoroutine(Coroutine(cooldown));
        }
    }

    IEnumerator Coroutine(float secs)
    {
        yield return new WaitForSeconds(secs);
        cooldownOver = true;
    }

    void SetNewRandomDestination()
    {
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        navAgent.SetDestination(navHit.position);

    }

    public EnemyState Enemy_State
    {
        get
        {
            return enemy_State;
        }
        set
        {
            enemy_State = value;
        }
    }
}
