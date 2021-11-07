using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTalkEvent : GameEvent {
    [SerializeField] GameObject visual;
    [SerializeField] Text text;
    [SerializeField] Text nameText;

    [SerializeField] private string[] talkCharactorName = new string[13];
    [SerializeField] private string[] talk = new string[13];
    private string endText;
    private string drawText;

    private int talkIndex = 0;
    private int textIndex = 0;
    private float addTextIntervalTimer = 0;
    private bool isSetCameraAction = true;
    // Start is called before the first frame update
    void Start() {
        isEvent = true;
        drawText = "";
        endText = "_";
    }

    // Update is called once per frame
    void Update() {

        Talk();

    }

    protected void Talk() {
        if (canEvent == true) {
            if (isEvent == true) {
                if (isSetCameraAction == true) {
                    isSetCameraAction = false;
                    //   StageManager.instance.camera.transform.position = new Vector3(0, 0, -15);
                    StageManager.instance.camera.SetAction<PointCameraAction>();
                    StageManager.instance.camera.GetAction<PointCameraAction>().SetPoint(StageManager.instance.player.gameObject.transform.position - Vector3.forward * 1.5f);//7
                }
                visual.SetActive(true);


                //1�������\������
                addTextIntervalTimer += Time.deltaTime;
                if (addTextIntervalTimer > 0.09f) {
                    if (textIndex < talk[talkIndex].Length) {
                        drawText += talk[talkIndex][textIndex];
                        addTextIntervalTimer = 0;
                        textIndex++;
                        endText = "_";

                    } else {
                        endText = "��";
                    }
                }

                //��b��i�߂�
                if (Input.GetKeyDown(KeyCode.Space) && textIndex >= talk[talkIndex].Length) {
                    //���̉�b
                    talkIndex++;
                    textIndex = 0;
                    drawText = "";
                } else if (Input.GetKeyDown(KeyCode.Space)) {
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
                    StageManager.instance.camera.SetAction<PlayerCameraAction>();
                    visual.SetActive(false);
                }


                nameText.text = talkCharactorName[talkIndex];
                text.text = "�u" + drawText + endText + "�v";
            }
        }
    }
}
