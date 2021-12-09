using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenuManager : MonoBehaviour
{
    void Start()
    {
        Screen.fullScreen = false;
        Screen.SetResolution(1280, 720, Screen.fullScreen, 60);
    }

    public void OnClickAdjustBigButton()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen, 60);
    }

    public void OnClickAdjustSmallButton()
    {
        Screen.SetResolution(1280, 720, Screen.fullScreen, 60);
    }

    public void OnClickAdjustFullMode()
    {
        Screen.fullScreen = true;
    }

    public void OnClickAdjustNotFullMode()
    {
        Screen.fullScreen = false;
    }
}
