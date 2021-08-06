using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUIManager : MonoBehaviour
{
    OpenDoorScript openScript;
    public Image red;
    public Image blue;

    void Start()
    {
        openScript = GameObject.Find(Tags.PLAYER_TAG).GetComponent<OpenDoorScript>();

        blue.enabled = false;
        red.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(openScript.hasBlueKey)
        {
            blue.enabled = true;
        }

        if(openScript.hasRedkey)
        {
            red.enabled = true;
        }
    }
}
