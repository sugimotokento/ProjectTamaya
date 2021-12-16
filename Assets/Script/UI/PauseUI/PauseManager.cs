using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// EventTrigger-System�g�����߂̂��
using UnityEngine.EventSystems;
// Scene�J�ڗp
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Panel�i�[�p
    [SerializeField] GameObject PauseMenuPanel;
    [SerializeField] GameObject toOptionMenuPanel;
    [SerializeField] GameObject GameEndMenuPanel;
    [SerializeField] GameObject NonePanel;
    // Filter�p
    [SerializeField] GameObject Filter;
    // PauseButton�i�[�p
    [SerializeField] GameObject PauseButton;
    // EventSystem�i�[�p�i�ǂ̃{�^���������ꂽ���𔻒肷�邽�߁j
    [SerializeField] private EventSystem eventSystem;
    // Panel�ޔ�p
    private GameObject nowPanel;
    private GameObject nextPanel;
    // Slide�p
    private bool SlideInFlg;
    private bool SlideOutFlg;
    private int SlideFrame;
    private float SlideMoveAmount;
    // SlideFlg�擾�p
    [HideInInspector] public bool isSlideActive = false;
    // ButtonAnimation�p
    private bool isPushFlg;
    private bool endPushFlg;
    private int AnimationFrame;
    private const float c_ScaleRatio = 0.2f;
    private const float c_ColorRatio = 0.5f;
    private GameObject PushButton;
    private GameObject EndPushButton;
    // FrameRatio�p
    private const int c_SlideFrameRatio = 10;
    private const int c_AnimFrameRatio = 5;
    // Button�̉����p
    private AudioSource PushButtonSE;
    private AudioSource EndPushButtonSE;

    void Start()
    {
        // Panel, SlideIn-Out, Filter, ButtonAnimation�̏����ݒ�
        nowPanel = nextPanel = NonePanel;
        SlideInFlg = SlideOutFlg = isSlideActive = false;
        SlideFrame = 0;
        Filter.SetActive(false);
        isPushFlg = endPushFlg = false;
        AnimationFrame = 0;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        PushButtonSE = audioSources[0];
        //EndPushButtonSE = audioSources[1];
    }

    void Update()
    {
        // PanelSlideIn-Out
        if (nowPanel != nextPanel)
            ChangeMenu();
        // ButtonAnimation
        if (isPushFlg || endPushFlg)
            ButtonAnimation();
        // Event���O��PauseButton�ݒ�
        if (StageManager.instance.isEventActive)
            PauseButton.SetActive(false);
        if (!StageManager.instance.isEventActive)
            PauseButton.SetActive(true);
    }

    public void OnClickPauseButton()
    {
        Debug.Log("PauseButton is Click!!");

        if (Time.timeScale != 0)
        {
            // �����ɉ��i�s�b�j�����Ă�
            // EndPushButtonSE.PlayOneShot(EndPushButtonSE.clip);

            Debug.Log("Pause!!");
            Time.timeScale = 0;
            Filter.SetActive(true);
            nextPanel = PauseMenuPanel;
        }
    }

    public void OnClicktoBack()
    {
        if (!SlideInFlg && !SlideOutFlg)
        {
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
        // �����ɉ��i�s�b�j�����Ă�
        // EndPushButtonSE.PlayOneShot(EndPushButtonSE.clip);

        nextPanel = toOptionMenuPanel;
    }

    public void OnClicktoTitleButton()
    {
        // �����ɉ��i�s�b�j�����Ă�
        // EndPushButtonSE.PlayOneShot(EndPushButtonSE.clip);
        // �^�C�g���ɖ߂鏈�������Ă�
        // SceneManager.LoadScene("Title");
    }

    public void OnClickGameEndButton()
    {
        // �����ɉ��i�s�b�j�����Ă�
        // EndPushButtonSE.PlayOneShot(EndPushButtonSE.clip);

        nextPanel = GameEndMenuPanel;
    }

    public void OnPushButton()
    {
        if (isPushFlg)
            return;

        // �����ɉ��i�|�b�j�����Ă�
        PushButtonSE.PlayOneShot(PushButtonSE.clip);

        isPushFlg = true;
        AnimationFrame = 0;
        PushButton = eventSystem.currentSelectedGameObject;
    }

    public void OnPointerUp()
    {
        endPushFlg = true;
        AnimationFrame = c_AnimFrameRatio;
    }

    // �{�� ����Ă邱�Ƃ�fade�Ɠ���
    private void ChangeMenu()
    {
        if (!SlideInFlg && !SlideOutFlg)
        {
            SlideOutFlg = true;
            isSlideActive = true;
            SlideFrame = 0;
        }

        if (SlideOutFlg)
        {
            // if you don't need SlideOut or SlideOut is allready finished, please start SlideIn.
            if (nowPanel == NonePanel || SlideFrame >= c_SlideFrameRatio)
            {
                SlideOutFlg = false;
                SlideInFlg = true;
                SlideFrame = 0;
                nowPanel.SetActive(false);
                if (nextPanel != NonePanel){
                    nextPanel.SetActive(true);
                    // Slide�̃t���[���ω��ʂ����W���狁�߂�
                    SlideMoveAmount = (Mathf.Abs(nextPanel.transform.position.x - this.transform.position.x) / (float)c_SlideFrameRatio);
                }
                return;
            }
            nowPanel.transform.position -= new Vector3(50.0f, 0.0f, 0.0f);
            SlideFrame++;
        }

        if (SlideInFlg)
        {
            if (nextPanel == NonePanel)
            {
                Debug.Log("UnPause!!");
                Time.timeScale = 1.0f;
                SlideInFlg = false;
                isSlideActive = false;
                Filter.SetActive(false);
                nowPanel = nextPanel;
                return;
            }
            if (SlideFrame >= c_SlideFrameRatio)
            {
                SlideInFlg = false;
                isSlideActive = false;
                nowPanel = nextPanel;
                return;
            }
            nextPanel.transform.position += new Vector3(SlideMoveAmount, 0.0f, 0.0f);
            SlideFrame++;
        }

    }

    private void ButtonAnimation()
    {
        if (isPushFlg)
        {
            // �k��
            float VariableScale = (1.0f / (float)c_AnimFrameRatio) * c_ScaleRatio * AnimationFrame;
            PushButton.transform.localScale = new Vector3(1.0f - VariableScale, 1.0f - VariableScale, 1.0f);
            // ���x�E�� (������GetComponent�����͋����āc)
            float VariableColor = 1.0f - (1.0f / (float)c_AnimFrameRatio) * c_ColorRatio * AnimationFrame;
            PushButton.GetComponent<Image>().color = new Color(VariableColor, VariableColor, VariableColor, 1.0f);

            // Animation�I������
            if (AnimationFrame >= c_AnimFrameRatio)
            {
                EndPushButton = PauseButton;
                isPushFlg = false;
            }
            else
                AnimationFrame++;
        }

        if (endPushFlg)
        {
            // �g��(Frame�̏����l=FrameRatio)
            float VariableScale = (1.0f / (float)c_AnimFrameRatio) * c_ScaleRatio * AnimationFrame;
            PushButton.transform.localScale = new Vector3(1.0f - VariableScale, 1.0f - VariableScale, 1.0f);
            // ���x�E�� (������GetComponent�����͋����āc)
            float VariableColor = 1.0f - (1.0f / (float)c_AnimFrameRatio) * c_ColorRatio * AnimationFrame;
            PushButton.GetComponent<Image>().color = new Color(VariableColor, VariableColor, VariableColor, 1.0f);

            if (AnimationFrame <= 0)
            {
                endPushFlg = false;
            }
            else
                AnimationFrame--;
        }
    }
}
