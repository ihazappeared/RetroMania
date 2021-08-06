using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToPlayer : MonoBehaviour
{
    private GameObject target;
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = Quaternion.LookRotation(target.transform.position - transform.position).eulerAngles;
        rot.x = rot.z = 0;
        this.transform.rotation = Quaternion.Euler(rot);
    }
}
