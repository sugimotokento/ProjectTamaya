using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    // PanelŠi”[—p
    [SerializeField] GameObject PauseMenuPanel;
    [SerializeField] GameObject toOptionMenuPanel;
    [SerializeField] GameObject GameEndMenuPanel;
    [SerializeField] GameObject NonePanel;
    // Panel‘Þ”ð—p
    private GameObject nowPanel;
    private GameObject nextPanel;
    // Slide—p
    private float SlideFrame;
    bool SlideInFlg;
    bool SlideOutFlg;

    void Start()
    {
        nowPanel = nextPanel = NonePanel;
        SlideFrame = 0.0f;
        SlideInFlg = SlideOutFlg = false;
    }

    void Update()
    {
        if (nowPanel != nextPanel)
            ChangeMenu();
    }

    public void OnClickPauseButton()
    {
        Debug.Log("PauseButton is Click!!");

        if (Time.timeScale != 0){
            Debug.Log("Pause!!");
            Time.timeScale = 0;
            nextPanel = PauseMenuPanel;
        }
    }

    public void OnClicktoBack()
    {
        if (!SlideInFlg && !SlideOutFlg) {
            if (nowPanel != PauseMenuPanel)
            {
                nextPanel = PauseMenuPanel;
            }
            else
            {
                nextPanel = NonePanel;
            }
        }
    }

    public void OnClicktoOptionButton()
    {
        nextPanel = toOptionMenuPanel;
    }

    public void OnClickGameEndButton()
    {
        nextPanel = GameEndMenuPanel;
    }

    // –{Š² ‚â‚Á‚Ä‚é‚±‚Æ‚Ífade‚Æ“¯‚¶
    private void ChangeMenu()
    {
        if (!SlideInFlg && !SlideOutFlg)
        {
            SlideOutFlg = true;
            SlideFrame = 0.0f;
        }

        if(SlideOutFlg)
        {
            // if you don't need SlideOut or SlideOut is allready finished, please start SlideIn.
            if(nowPanel == NonePanel || SlideFrame >= 1.0f)
            {
                SlideOutFlg = false;
                SlideInFlg = true;
                SlideFrame = 0.0f;
                nowPanel.SetActive(false);
                if(nextPanel != NonePanel)
                    nextPanel.SetActive(true);
                return;
            }
            nowPanel.transform.position -= new Vector3(50.0f, 0.0f, 0.0f);
            SlideFrame += 0.1f;
        }

        if (SlideInFlg)
        {
            if (nextPanel == NonePanel)
            {
                Debug.Log("UnPause!!");
                Time.timeScale = 1.0f;
                SlideInFlg = false;
                nowPanel = nextPanel;
                return;
            }
            if (SlideFrame >= 1.0f)
            {
                SlideInFlg = false;
                nowPanel = nextPanel;
                return;
            }
            nextPanel.transform.position += new Vector3(50.0f, 0.0f, 0.0f);
            SlideFrame += 0.1f;
        }

    }
}
