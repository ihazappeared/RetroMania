using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneController : MonoBehaviour
{
    private Animator _animator;

    private NavMeshAgent _navMeshAgent;

    private GameObject Player;

    public float AttackDistance = 1.5f;

    public float FollowDistance = 20.0f;

    public float DamagePoints = 2.0f;

    public float cooldownSecs = 3.0f;
    private bool cooldownOver;

    private bool follow;
    private bool chase;

    public float dist;

    public float patrol_For_This_Time = 5f;
    private float patrol_Timer;

    public float walk_speed = 0.5f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;

    RaycastHit vRaycast;

    private Transform target;

    void Awake()
    {
        Player = GameObject.Find("Player");
        target = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;

        _navMeshAgent = GetComponent<NavMeshAgent>();

        _animator = GetComponentInChildren<Animator>();
        cooldownOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent.enabled)
        {
            dist = Vector3.Distance(Player.transform.position, this.transform.position);
            bool shoot = false;

            if (Physics.Raycast(_navMeshAgent.transform.position, (Player.transform.position - _navMeshAgent.transform.position), out vRaycast))
            {
                if (vRaycast.transform == target)
                {
                    Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.green);
                }
                else
                {
                    Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.red);
                }

            }

            if (dist < FollowDistance && Physics.Raycast(_navMeshAgent.transform.position, (Player.transform.position - _navMeshAgent.transform.position), out vRaycast) && cooldownOver)
            {
                if(vRaycast.transform == target)
                    follow = true;
            }
            else
            {
                Patrol();
            }

            if (follow)
            {
                if (dist <= AttackDistance && cooldownOver)
                {
                    shoot = true;
                    _navMeshAgent.speed = walk_speed;
                    _navMeshAgent.SetDestination(Player.transform.position);
                }
                else if(dist <= AttackDistance && !cooldownOver)
                {
                    Vector3 rot = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
                    rot.x = rot.z = 0;
                    transform.rotation = Quaternion.Euler(rot);

                    //Stop Enemy movement
                    _navMeshAgent.velocity = Vector3.zero;
                    _navMeshAgent.isStopped = true;
                }
                
            }

            if(shoot && cooldownOver)
            {
                Shoot();
            }
            else
            {
                Patrol();
            }

            if (!follow)
            {
                Patrol();
            }
        }
        else
        {
            Debug.LogError("Navmesh agent: " + this.gameObject.name + " not working");
        }
    }

    private void Patrol()
    {
        //tell navAgent that enemy can move
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = walk_speed;

        patrol_Timer += Time.deltaTime;

        if (patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();
            patrol_Timer = 0f;
        }

        if (_navMeshAgent.velocity.sqrMagnitude > 0)
        {
            _animator.SetBool(AnimationTags.WALK_PARAMETER, true);
        }
        else
        {
            _animator.SetBool(AnimationTags.WALK_PARAMETER, false);
        }
        if(dist < FollowDistance && Physics.Raycast(_navMeshAgent.transform.position, (Player.transform.position - _navMeshAgent.transform.position), out vRaycast) && cooldownOver)
        {
            if(vRaycast.transform == target)
                    follow = true;
        }
    }

    void SetNewRandomDestination()
    {
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        _navMeshAgent.SetDestination(navHit.position);

    }

    private void Shoot()
    {
        //Look at Player
        Vector3 rot = Quaternion.LookRotation(Player.transform.position - transform.position).eulerAngles;
        rot.x = rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);

        _navMeshAgent.SetDestination(transform.position);
        _animator.SetTrigger("Attack");

        cooldownOver = false;
        StartCoroutine(Coroutine(cooldownSecs));
        GetComponentInChildren<BulletCreator>().shoot = true;
    }

    IEnumerator Coroutine(float secs)
    {
        yield return new WaitForSeconds(secs);
        cooldownOver = true;
    }
}
