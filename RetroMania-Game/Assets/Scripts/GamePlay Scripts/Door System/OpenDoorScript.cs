using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorScript : MonoBehaviour
{
    private Camera mainCam;
    private Animator anim_Door;

    GameObject door;
    DoorHelper doorScript;

    [SerializeField]
    private AudioSource doorOpenSound;
    [SerializeField]
    private AudioSource doorCloseSound;

    private bool open;
    public bool hasBlueKey;
    public bool hasRedkey;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, 3))
            {
                if (hit.transform.tag == Tags.DOOR_TAG && open == false)
                {
                    door = hit.transform.gameObject;

                    anim_Door = door.GetComponent<Animator>();
                    if(open == false)
                    {
                        doorScript = door.GetComponentInParent<DoorHelper>();
                        doorScript.lockCheck(door);
                    }
                }
            }
        }
    }
}
