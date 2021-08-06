using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTypeScript : MonoBehaviour
{
    public bool blueType;
    public bool redType;

    PickupSoundScript sound;

    OpenDoorScript openScript;

    // Start is called before the first frame update
    void Start()
    {
        openScript = GameObject.Find(Tags.PLAYER_TAG).GetComponent<OpenDoorScript>();
        sound = GameObject.Find("AudioManager").GetComponentInChildren<PickupSoundScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == Tags.PLAYER_TAG)
        {
            if (blueType)
            {
                openScript.hasBlueKey = true;
            }
            else if (redType)
            {
                openScript.hasRedkey = true;
            }

            this.gameObject.SetActive(false);
            sound.PlayPickUpSound();
        }
    }

    
}
