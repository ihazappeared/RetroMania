using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    private Animator anim;
    private GameObject Player;

    private bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        anim = GetComponent<Animator>();

        Player.transform.parent = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            Player.transform.parent = transform;
            Debug.Log("Player entered elevator");

            if(firstTime)
            {
                firstTime = false;
                anim.SetTrigger("Start");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }

}
