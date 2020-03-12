using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuContoller : MonoBehaviour
{
    public Button fullscreenButton;
    public TMP_Dropdown resolutionDropdown;

    private bool isFullscreen;
    private Resolution[] resolutions;

    void Start()
    {
        isFullscreen = Screen.fullScreen;
        SetFullscreenButtonText();

        int currentResolution = 0;
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resolutionOptions.Add(option);
            if (resolutions[i].Equals(Screen.currentResolution))
                currentResolution = i;
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetGraphics(int graphicsLevelIndex)
    {
        QualitySettings.SetQualityLevel(graphicsLevelIndex);
    }

    public void SetFullscreen()
    {
        isFullscreen = !isFullscreen;
        Screen.fullScreen = isFullscreen;
        SetFullscreenButtonText();
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    private void SetFullscreenButtonText()
    {
        if (isFullscreen)
            fullscreenButton.GetComponentInChildren<TextMeshProUGUI>().text = "ON";
        else fullscreenButton.GetComponentInChildren<TextMeshProUGUI>().text = "OFF";
    }
}
