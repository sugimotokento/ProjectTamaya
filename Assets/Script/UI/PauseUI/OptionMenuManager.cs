using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenuManager : MonoBehaviour
{
    // ‰ð‘œ“xButton
    [SerializeField] GameObject AdjustBigButton;
    [SerializeField] GameObject AdjustSmallButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnClickAdjustBigButton()
    {
        Screen.SetResolution(1920, 1080, false, 60);
    }

    public void OnClickAdjustSmallButton()
    {
        Screen.SetResolution(1280, 720, false, 60);
    }

}
