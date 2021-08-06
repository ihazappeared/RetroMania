using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{

    Resolution[] resolutions;

    int resolutionIndex;

    void Awake()
    {
        // STARTING SCRIPTS - Only load on scene start
        GetResolution();
    }

    void GetResolution()
    {
        //Gets resolution index saved and compares to Unity resolutions array
        resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate >= 60).ToArray();

        //Sets resolution received
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

}
