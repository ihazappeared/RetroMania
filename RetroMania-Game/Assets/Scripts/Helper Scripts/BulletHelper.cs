using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHelper : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    [HideInInspector]
    public float shotBy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == Tags.ENEMY_TAG && shotBy != 1 || other.gameObject.tag == Tags.PLAYER_TAG )
        {
            other.gameObject.GetComponent<HealthScript>().ApplyDamage(damage);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.name != "BulletEmitter")
        {
            Destroy(this.gameObject);
        }
    }
}
