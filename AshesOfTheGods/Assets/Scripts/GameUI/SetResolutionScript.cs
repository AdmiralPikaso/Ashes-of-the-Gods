using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetResolutionScript : MonoBehaviour
{
   

    [SerializeField] private TMP_Dropdown resoulutionDP;
    [SerializeField] private Toggle fullScreenToggle;

    private void Start()
    {
        bool fullScreen = Screen.fullScreen;
        Screen.fullScreen = true;

        int width = Screen.currentResolution.width;
        int height = Screen.currentResolution.height;


        if (width == 1280 & height == 720)
            resoulutionDP.value = 0;

        else if (width == 1366 & height == 768)
            resoulutionDP.value = 1;
        
        else if (width == 1600 & height == 900)
            resoulutionDP.value = 2;
        
        else if (width == 1920 & height == 1080)
            resoulutionDP.value = 3;
        
        else if (width == 2160 & height == 1080)
            resoulutionDP.value = 4;
        
        else if (width == 2560 & height == 1440)
            resoulutionDP.value = 5;
        
        else if (width == 2560 & height == 1600)
            resoulutionDP.value = 6;
        
        else if (width == 2960 & height == 1664)
            resoulutionDP.value = 7;
        
        else if (width == 2960 & height == 1440)
            resoulutionDP.value = 8;
        
        else if (width == 3024 & height == 1964)
            resoulutionDP.value = 9;
        
        else if (width == 3200 & height == 1800)
            resoulutionDP.value = 10;
        
        else if (width == 3456 & height == 2234)
            resoulutionDP.value = 11;
        
        else if (width == 3840 & height == 2160)
            resoulutionDP.value = 12;

        
        fullScreenToggle.isOn = fullScreen;
    }
    public void ChangeResolution()
    {
        switch (resoulutionDP.value)
        {
            case 0:
                Screen.SetResolution(1280, 720, fullScreenToggle.isOn);
                break;

            case 1:
                Screen.SetResolution(1366, 768, fullScreenToggle.isOn);
                break;
            case 2:
                Screen.SetResolution(1600,900, fullScreenToggle.isOn);
                break;
            case 3:
                Screen.SetResolution(1920, 1080, fullScreenToggle.isOn);
                break;
            case 4:
                Screen.SetResolution(2160, 1080, fullScreenToggle.isOn);
                break;
            
            case 5:
                Screen.SetResolution(2560,1440,fullScreenToggle.isOn);
                break;
            case 6:
                Screen.SetResolution(2560, 1600, fullScreenToggle.isOn);
                break;
            case 7:
                Screen.SetResolution(2560, 1664, fullScreenToggle.isOn);
                break;
            case 8:
                Screen.SetResolution(2960,1440,fullScreenToggle.isOn);
                break;
            case 9:
                Screen.SetResolution(3024, 1964, fullScreenToggle.isOn);
                break;
            case 10:
                Screen.SetResolution(3200,1800,fullScreenToggle.isOn);
                break;
            case 11:
                Screen.SetResolution(3456, 2234, fullScreenToggle.isOn);
                break;
            case 12:
                Screen.SetResolution(3840, 2160, fullScreenToggle.isOn);
                break;
        }
    }

    
    public void FullScreenMode()
    { 
            Screen.fullScreen = fullScreenToggle.isOn;
            
    }


}
