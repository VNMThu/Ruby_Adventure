using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GraphicController : MonoBehaviour
{
    public Toggle fullScreenToggle;
    int currentWidth;
    int currentHeight;
    public TMP_Dropdown resolutionDropdown;
    // Start is called before the first frame update
    private void Start()
    {
        fullScreenToggle.isOn = Screen.fullScreen;
        currentWidth = Screen.currentResolution.width;
        currentHeight = Screen.currentResolution.height;
        StartResolution();
    }

    // Update is called once per frame
    public void ChangeFullScene()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ChangeResolution()
    {
        switch (resolutionDropdown.value)
        {
            case 0:
                currentWidth = 800;
                currentHeight = 600;
                break;
            case 1:
                currentWidth = 1368;
                currentHeight = 768;
                break;
            case 2:
                currentWidth = 1920;
                currentHeight = 1080;
                break;
            default:
                break;
        }
        Screen.SetResolution(currentWidth, currentHeight, Screen.fullScreen);

    }

    private void StartResolution()
    {
        switch (currentWidth)
        {
            case 800:
                resolutionDropdown.value = 0;
                break;
            case 1920:
                resolutionDropdown.value = 2;
                break;
            case 1368:
                resolutionDropdown.value = 1;
                break;
            default:
                break;
        }
    }

}
