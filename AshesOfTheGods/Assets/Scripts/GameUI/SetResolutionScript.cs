using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetResolutionScript : MonoBehaviour
{
    

    [SerializeField] private TMP_Dropdown resoulutionDP;
    [SerializeField] private Toggle fullScreenToggle;
    
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
