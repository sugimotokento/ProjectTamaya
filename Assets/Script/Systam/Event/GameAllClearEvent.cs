using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameAllClearEvent : GameEvent
{

    public GameObject ropeUpper;
    public GameObject ropeRight;
    public GameObject logo;
    public GameObject clear;
 
    public GameObject stageScore;

    
    public GameObject ranktxt;
    public GameObject rankimg;
    public GameObject result;
    public GameObject click;

    [SerializeField] GameObject soundObj;
    [SerializeField] Sprite[] rankSprite;
    [SerializeField] ImageNumber[] stageScoreNum=new ImageNumber[5];
    [SerializeField] ImageNumber allScore;

    [SerializeField] float ropeMoveSpeed;
    [SerializeField] float logoRotateTime;
    [SerializeField] int logoRotatePerSec;
    [SerializeField] int rankAnimStartCount;
    [SerializeField] float rankChangeSpeed;
    [SerializeField] float clickMoveSpeed;
    [SerializeField] FadeOut2 fade;

    List<AudioSource> sound = new List<AudioSource>();
    bool canPlaySE = true;
    bool canPlaySE2 = true;
    bool canPlayBGM = true;
    bool isNextScene = false;

    // logoRotateTime����
    // ��~�^�C�~���O��scale��1.0�𒴂����^�C�~���O�ŉ�]�E�g�傪�~�܂�̂�
    // ���r���[�ȕb�����Ɗp�x���Y���܂��B ���X�X����0.6��1�B
    // �P�ʂ�"�b"�B��₱�����ˁB

    struct phaseMode {
        public bool ready;
        public bool isPlay;
        public bool isEnd;
    };

    enum phaseName {
        FADEIN_ROPE,
        ROTATE_LOGO,
        FADEIN_RESULT,
        FADEIN_RANK,
        FADEIN_CLICKNEXT,
        MAX
    };

    phaseMode[] phase = new phaseMode[(int)phaseName.MAX];
    [SerializeField] int[] fadeFrame = new int[(int)phaseName.MAX];
    void Start() {
        //Application.targetFrameRate = 60;
        // �e�I�u�W�F�N�g��t���[���O�Ɉړ�������
        Vector3 defaultPos = ropeUpper.transform.localPosition;
        defaultPos.x -= 2000.0f;
        ropeUpper.transform.localPosition = defaultPos;

        defaultPos = ropeRight.transform.localPosition;
        defaultPos.x += 2000.0f;
        ropeRight.transform.localPosition = defaultPos;

        defaultPos = clear.transform.localPosition;
        defaultPos.x -= 2000.0f;
        clear.transform.localPosition = defaultPos;

        logo.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        defaultPos = result.transform.localPosition;
        defaultPos.x -= 2000f;
        result.transform.localPosition = defaultPos;

        defaultPos = stageScore.transform.localPosition;
        defaultPos.x -= 2000f;
        stageScore.transform.localPosition = defaultPos;


        rankimg.transform.localScale = new Vector3(2.5f, 2.5f, 0.0f);

        defaultPos = click.transform.localPosition;
        defaultPos.y -= 100f;
        click.transform.localPosition = defaultPos;

        for (int i = 0; i < soundObj.transform.childCount; ++i) {
            sound.Add(soundObj.transform.GetChild(i).GetComponent<AudioSource>());
        }
    }

    // Update is called once per frame
    void Update() {
        if (StageManager.instance.boss.hp<=0) {
            Boss boss = StageManager.instance.boss;
            if (boss.CheckAction<BossDieAction>() == true && boss.GetAction<BossDieAction>().isDieAnimationEnd == true) {
                isEvent = true;
                phase[0].ready = true;
            }
        }
        if (canEvent == true) {
            if (canPlayBGM == true) {
                canPlayBGM = false;
                sound[0].Play();
            }
            for(int i=0; i<5; i++) {
                stageScoreNum[i].SetNumber(ScoreManager.stageScore[i]);
            }
            allScore.SetNumber(ScoreManager.GetAllScore());
            rankimg.GetComponent<Image>().sprite = rankSprite[(int)ScoreManager.GetAllRank()];
            //�����ɏ���
            transform.GetChild(0).gameObject.SetActive(true);

            // ��������e����
            if (phase[0].ready && !phase[0].isEnd) {
                if (ropeUpper.transform.localPosition.x >= 0.0f) moveNextPhase(0);
                else {
                    ropeUpper.transform.position += new Vector3(ropeMoveSpeed * Time.deltaTime, 0f, 0f);
                    ropeRight.transform.position += new Vector3(-ropeMoveSpeed * Time.deltaTime, 0f, 0f);
                    clear.transform.position += new Vector3(ropeMoveSpeed * Time.deltaTime, 0f, 0f);
                }
            }

            if (phase[(int)phaseName.ROTATE_LOGO].ready && !phase[(int)phaseName.ROTATE_LOGO].isEnd) {

                if (logo.transform.localScale.x <= 1.0f) {
                    float rotateTime = 0.2f / ((float)logoRotateTime);
                    logo.transform.Rotate(0f, 0f, -360.0f * logoRotatePerSec * Time.deltaTime);
                    logo.transform.localScale += new Vector3(rotateTime * Time.deltaTime, rotateTime * Time.deltaTime, 0.0f);
                } else {
                    // �����ɂ��������̏���������΂�����Y��ɂȂ�
                    moveNextPhase((int)phaseName.ROTATE_LOGO);
                }
            }

            if (phase[(int)phaseName.FADEIN_RESULT].ready && !phase[(int)phaseName.FADEIN_RESULT].isEnd) {
                if (result.transform.localPosition.x >= -598.0f) moveNextPhase((int)phaseName.FADEIN_RESULT);
                else {
                    result.transform.position += new Vector3(ropeMoveSpeed * Time.deltaTime, 0f, 0f);
                    stageScore.transform.position += new Vector3(ropeMoveSpeed * Time.deltaTime, 0f, 0f);

                }
            }

            if (phase[(int)phaseName.FADEIN_RANK].ready && !phase[(int)phaseName.FADEIN_RANK].isEnd) {
                if (canPlaySE == true) {
                    sound[1].Play();
                    canPlaySE = false;
                }
                if (rankAnimStartCount != 0) rankAnimStartCount--;
                else {
                    rankimg.SetActive(true);
                    ranktxt.SetActive(true);
                    if (rankimg.transform.localScale.x <= 1.0f) moveNextPhase((int)phaseName.FADEIN_RANK);
                    else {
                        rankimg.transform.localScale -= new Vector3(rankChangeSpeed * Time.deltaTime, rankChangeSpeed * Time.deltaTime, 0.0f);

                    }
                }
            }

            if (phase[(int)phaseName.FADEIN_CLICKNEXT].ready && !phase[(int)phaseName.FADEIN_CLICKNEXT].isEnd) {
                if (canPlaySE2 == true) {
                    sound[1].Play();
                    canPlaySE2 = false;
                }
                if (click.transform.localPosition.y <= -480.0f) {
                    click.transform.position += new Vector3(0f, clickMoveSpeed * Time.deltaTime, 0f);
                } else {
                    // �I������
                    //  moveNextPhase((int)phaseName.FADEIN_CLICKNEXT);
                    if (Input.GetMouseButtonDown(0)) {
                        isNextScene = true;
                        fade.gameObject.SetActive(true);
                    }
                    if (fade.GetIsFadeEnd() == true) {

                        SceneManager.LoadScene("Clear");
                    }
                }
            }
        }
    }

    void moveNextPhase(int index) {
        if (index < (int)phaseName.MAX - 1) {
            phase[index].isEnd = true;
            phase[index + 1].ready = true;

        } else {
            phase[index].isEnd = true;
        }
        return;
    }
}
