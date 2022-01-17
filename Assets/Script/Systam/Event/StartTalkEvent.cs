using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTalkEvent : GameEvent {
    [SerializeField] GameObject visual;

    [SerializeField] Text text;
    [SerializeField] Image frameRight;
    [SerializeField] Image frameLeft;
    [SerializeField] Image toshiro;
    [SerializeField] Image rebecca;
    [SerializeField] Image okami;
    [SerializeField] Text nameRightText;
    [SerializeField] Text nameLeftText;

    [SerializeField] AudioSource sound;

    [SerializeField] private string[] talkCharactorName = new string[13];
    [SerializeField] private string[] talk = new string[13];

    [SerializeField] private bool isEndEvent = false;

    private string endText;
    private string drawText;

    private int talkIndex = 0;
    private int textIndex = 0;
    private float addTextIntervalTimer = 0;
    private bool isSetCameraAction = true;
    private bool canPlayBGM = true;
    private bool isEventOnce = false;

    // Start is called before the first frame update
    void Start() {
        if (isEndEvent == false) {
            isEvent = true;
        }
        drawText = "";
        endText = "_";


    }

    // Update is called once per frame
    void Update() {
        if (StageManager.instance.boss != null && isEventOnce == false) {
            Boss boss = StageManager.instance.boss;
            if (boss.CheckAction<BossDieAction>() == true && boss.GetAction<BossDieAction>().isDieAnimationEnd == true) {
                isEvent = true;
                isEventOnce = true;
            }
        }
        Talk();

    }

    protected void Talk() {
        if (canEvent == true) {
            if (isEvent == true) {
                isEventOnce = true;
                if (canPlayBGM == true) {
                    canPlayBGM = false;
                    StageManager.instance.bgm.Stop();
                    StageManager.instance.bgm.clip = StageManager.instance.eventBGMClip;
                    StageManager.instance.bgm.Play();
                }
                if (isSetCameraAction == true) {
                    isSetCameraAction = false;
                    //   StageManager.instance.camera.transform.position = new Vector3(0, 0, -15);
                    StageManager.instance.camera.SetAction<PointCameraAction>();
                    StageManager.instance.camera.GetAction<PointCameraAction>().SetPoint(StageManager.instance.player.gameObject.transform.position - Vector3.forward * 6);
                }
                visual.SetActive(true);
              
                //���E�̃L�����N�^�[����
                if (talkCharactorName[talkIndex] == "�g�V���[") {
                    toshiro.color = new Color(1, 1, 1, 1);
                    rebecca.color = new Color(1, 1, 1, 0.3f);
                    okami.color = new Color(1, 1, 1, 0);
                    nameLeftText.gameObject.SetActive(true);
                    frameLeft.gameObject.SetActive(true);

                    nameRightText.gameObject.SetActive(false);
                    frameRight.gameObject.SetActive(false);


                } else {

                    nameRightText.gameObject.SetActive(true);
                    frameRight.gameObject.SetActive(true);

                    nameLeftText.gameObject.SetActive(false);
                    frameLeft.gameObject.SetActive(false);
                }


                if (talkCharactorName[talkIndex] == "���x�b�J") {
                    toshiro.color = new Color(1, 1, 1, 0.3f);
                    rebecca.color = new Color(1, 1, 1, 1);
                    okami.color = new Color(1, 1, 1, 0);

                } else if (talkCharactorName[talkIndex] == "�g�V���[") {

                } else if (talkCharactorName[talkIndex] == "�I�J�~") {
                    toshiro.color = new Color(1, 1, 1, 0.3f);
                    rebecca.color = new Color(1, 1, 1, 0);
                    okami.color = new Color(1, 1, 1, 1);

                } else {
                    toshiro.color = new Color(1, 1, 1, 0.3f);
                    rebecca.color = new Color(1, 1, 1, 0.3f);
                    okami.color = new Color(1, 1, 1, 0);

                }


                //1�������\������
                addTextIntervalTimer += Time.deltaTime;
                if (addTextIntervalTimer > 0.09f) {
                    if (textIndex < talk[talkIndex].Length) {
                        drawText += talk[talkIndex][textIndex];
                        addTextIntervalTimer = 0;
                        textIndex++;
                        endText = "_";

                        sound.Play();

                    } else {
                        endText = "��";
                    }
                }

                //��b��i�߂�
                if (Input.GetMouseButtonDown(0) && textIndex >= talk[talkIndex].Length) {
                    //���̉�b
                    talkIndex++;
                    textIndex = 0;
                    drawText = "";
                } else if (Input.GetMouseButtonDown(0)) {
                    //���̉�b��S�ĕ\��
                    textIndex = talk[talkIndex].Length;
                    drawText = talk[talkIndex];
                }


                //��b�̏I��
                SkipEvent();
                if (talkIndex >= talk.Length || isEvent == false) {
                    talkIndex = talk.Length - 1;
                    isEvent = false;
                    canEvent = false;
                    visual.SetActive(false);
                    StageManager.instance.camera.SetAction<PlayerCameraAction>();
                    StageManager.instance.bgm.Stop();
                    StageManager.instance.bgm.clip = StageManager.instance.mainBGMClip;
                    StageManager.instance.bgm.Play();

                }

                if (talkCharactorName[talkIndex] == "�g�V���[") {
                    nameLeftText.text = talkCharactorName[talkIndex];
                } else {
                    nameRightText.text = talkCharactorName[talkIndex];
                }
                text.text = "�u" + drawText + endText + "�v";
            }
        }
    }
}
