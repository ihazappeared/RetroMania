using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairActivator : MonoBehaviour
{
    public Image crosshair;
    private RaycastHit hit;
    private Camera mainCam;
    private Animator anim;

    void Start()
    {
        mainCam = Camera.main;
        anim = crosshair.GetComponent<Animator>();
    }

    void Update()
    {
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, 3))
        {
            if (hit.transform.gameObject.tag == "Pickup")
            {
                anim.SetBool("Hover", true);
            }
            else
            {
                anim.SetBool("Hover", false);
            }
        }

    }
}
