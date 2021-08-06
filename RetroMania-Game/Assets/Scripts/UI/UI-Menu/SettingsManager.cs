using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    Resolution[] resolutions;

    public Dropdown resDropdown;
    public Toggle fullScreenToggle;
    public Slider volumeSlider;

    private float globalVolume;

    void Start()
    {
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate >= 60).ToArray();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        foreach (var res in resolutions)
        {
            Debug.Log(res.width + "x" + res.height + " : " + res.refreshRate);
        }

        resDropdown.ClearOptions();
        Debug.Log("Total Resolutions:" + resolutions.Length);

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (!resDropdown.GetComponent<Dropdown>().options.Contains(new Dropdown.OptionData(ResToString(resolutions[i]))))
            {
                resDropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));

                Debug.Log("value = " + resDropdown.GetComponent<Dropdown>().value);
                resDropdown.GetComponent<Dropdown>().value = i;
            }

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();

        audioMixer.GetFloat("volume", out globalVolume);
        globalVolume = Mathf.Pow(10, globalVolume / 20);
        volumeSlider.value = globalVolume;

        if(Screen.fullScreen == true)
        {
            fullScreenToggle.isOn = true;
        }
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10 (volume) * 20);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        PlayerPrefs.SetInt("QualityIndex", qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        Debug.Log("Resolution changed to: " + resolution.width + " x " + resolution.height);
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }
}
