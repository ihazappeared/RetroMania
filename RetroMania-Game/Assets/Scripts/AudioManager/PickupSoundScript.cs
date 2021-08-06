using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSoundScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource pickupSound;

    public void PlayPickUpSound()
    {
        pickupSound.Play();
    }
}
