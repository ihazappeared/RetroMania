using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHelper : MonoBehaviour
{
    GameObject door;

    [SerializeField]
    private AudioSource doorOpenSound;
    [SerializeField]
    private AudioSource doorCloseSound;

    private BoxCollider col;

    private Animator anim_Door;
    OpenDoorScript openScript;
    LevelFinish finishScript;

    [HideInInspector]
    public bool open;

    public bool noType;
    public bool blueType;
    public bool redType;
    public bool isFinishDoor;

    //[HideInInspector]
    public bool unlocked;
    private bool Checked;

  

    void Start()
    {
        openScript = GameObject.Find(Tags.PLAYER_TAG).GetComponent<OpenDoorScript>();
        try
        {
            finishScript = GameObject.Find("--- UI ---").GetComponent<LevelFinish>();
        }
        catch (Exception e)
        {
            Debug.LogError("UI not present (LevelFinish) - Script: PickupScript\n" + e);
        }

        col = this.GetComponent<BoxCollider>();

        anim_Door = this.GetComponent<Animator>();

        open = false;

        if(noType)
        {
            unlocked = true;
        }
        else
        {
            unlocked = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Checked = false;
        door = this.transform.gameObject;
        
        if (other.transform.gameObject.tag == Tags.ENEMY_TAG)
        {
            if(open == false)
                OpenDoor();
        }
    }

    public void lockCheck(GameObject door1)
    {
        door = door1;
        Checked = true;

        if (isFinishDoor)
        {
            finishScript.active = true;
        }

        if (!open)
        {
            if (blueType)
            {
                if (openScript.hasBlueKey)
                {
                    unlocked = true;
                    if (unlocked)
                    {
                        OpenDoor();
                    }
                }
            }


            if (redType)
            {
                if (openScript.hasRedkey)
                {
                    unlocked = true;
                    if (unlocked)
                    {
                        OpenDoor();
                    }
                }
            }

            if (noType)
            {
                OpenDoor();
            }
        }
    }

    public void OpenDoor()
    {
        open = true;
        anim_Door.SetTrigger("Open");
        doorOpenSound.Play();

        col.enabled = false;

        StartCoroutine(Coroutine(2));
    }

    IEnumerator Coroutine(int secs)
    {
        yield return new WaitForSeconds(secs);
        CloseDoor();
    }

    public void CloseDoor()
    {
        open = false;
        anim_Door.SetTrigger("Close");
        doorCloseSound.Play();

        col.enabled = true;
    }
}
