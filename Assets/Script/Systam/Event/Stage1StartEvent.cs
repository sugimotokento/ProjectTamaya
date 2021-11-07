using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1StartEvent : GameEvent {
    [SerializeField] GameObject visual;
    [SerializeField] Text text;
    [SerializeField] Text nameText;

    private string[] talkCharactor = new string[13];
    private string[] talk = new string[13];
    private string endText;
    private string drawText;

    private int talkIndex = 0;
    private int textIndex = 0;
    private float addTextIntervalTimer = 0;

    // Start is called before the first frame update
    void Start() {
        eventPos=(StageManager.instance.player.gameObject.transform.position - Vector3.forward * 5);

        drawText = "";
        endText = "_";
        talkCharactor[0] = "�g�V���[";
        talk[0] = "���ނ����΂����Ȃ̂Ƀg���u���Ɋ������܂����āE�E�E\n��������ł��߂��Ă�̂�������˂��A���x���P���ł��s����";

        talkCharactor[1] = "�H�H�H";
        talk[1] = "���́I�I�I�I";

        talkCharactor[2] = "�g�V���[";
        talk[2] = "��A�N���H";

        talkCharactor[3] = "�H�H�H";
        talk[3] = "���߂܂��āA���Ȃ��͌�OKPK�̃g�V���[����ł���ˁH";

        talkCharactor[4] = "�g�V���[";
        talk[4] = "�����A�����Ȃ�ł���Ȏ��m���Ă���񂾁H";

        talkCharactor[5] = "�r�V���b�v";
        talk[5] = "�\���x��܂����A����͐V��OKPK�����̃r�V���b�v�ƌ����܂��I\n�����̍��̃g�V���[����̊�����Ղ�ɓ���Ă�����ł���";

        talkCharactor[6] = "�g�V���[";
        talk[6] = "�Ȃ�قǁA���Ⴀ����̑����͌N�ɔC���Ă������Ă��Ƃ��ȁH";

        talkCharactor[7] = "�r�V���b�v";
        talk[7] = "���ꂪ�E�E�E����͂܂��C���ɓ����������Ƃ��Ȃ��A\n�|���ē����Ȃ���ł��E�E�E";

        talkCharactor[8] = "�g�V���[";
        talk[8] = "�������A�̂̉������Ă���悤���ȁE�E�E\n�悵�A����͉��ɔC����I�r�V���b�v�͒����Ă��Ă���!\n�d���_�͎����Ă邩�H";

        talkCharactor[9] = "�r�V���b�v";
        talk[9] = "���́A�͂��I�ǂ����I";

        talkCharactor[10] = "�g�V���[";
        talk[10] = "�V�i�Ńs�b�J�s�J�̓d���_���ȁI\n�E�E�E��H���������Ă��̂Ƃ������`�Ƃ��Ⴄ�݂�����";

        talkCharactor[11] = "�r�V���b�v";
        talk[11] = "�V�đ����p�̓d���_�ɂ�AR�ōĐ������\n����v���O�������g�ݍ��܂�Ă����ł���";

        talkCharactor[12] = "�g�V���[";
        talk[12] = "�ւ��E�E�E���J�Ȃ������B���S�ɖ߂��Ă݂邩";
    }

    // Update is called once per frame
    void Update() {
        if (isEvent == true) {
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
            if (talkIndex >= talk.Length) {
                talkIndex = talk.Length - 1;
                isEventEnd = true;
                isEvent = false;
                visual.SetActive(false);
            }
            SkipEvent();

            nameText.text = talkCharactor[talkIndex];
            text.text     = "�u" + drawText + endText + "�v";
        } else {
            visual.SetActive(false);
        }
    }
}
