using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAnimManager : MonoBehaviour {

    [Header("ANIMATORS")]
    public Animator panelAnimator;

    [Header("ANIMATION STRINGS")]
    public string fadeInAnim;
    public string fadeOutAnim;

    [Header("SETTINGS")]
    public string shortcutKey;

    private bool isOn = false;

    void Update()
    {
        if (Input.GetKeyDown(shortcutKey) && isOn == true)
        {
            panelAnimator.Play(fadeOutAnim);
            isOn = false;
        }
        else if (Input.GetKeyDown(shortcutKey) && isOn == false)
        {
            panelAnimator.Play(fadeInAnim);
            isOn = true;
        }
    }

    public void AnimatePanel ()
    {
        if (isOn == true)
        {
            panelAnimator.Play(fadeOutAnim);
            isOn = false;
        }
        else if (isOn == false)
        {
            panelAnimator.Play(fadeInAnim);
            isOn = true;
        }
    }
}
