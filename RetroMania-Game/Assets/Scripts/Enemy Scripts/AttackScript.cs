using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float damage = 2f;
    public float radius = 1f;
    public LayerMask layerMask;

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);

        if(hits.Length > 0)
        {
            if(hits[0].gameObject.GetComponent<HealthScript>().health < damage)
            {
                hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(hits[0].gameObject.GetComponent<HealthScript>().health);
            }
            else
            {
                hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);
            }
            //hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);
            print("Touched:" + hits[0].gameObject.tag);
            gameObject.SetActive(false);

        }
    }
}
