using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearEvent : GameEvent {

    public GameObject ropeUpper;
    public GameObject ropeRight;
    public GameObject logo;
    public GameObject clear;
    public GameObject air;
    public GameObject score;
    public GameObject ranktxt;
    public GameObject rankimg;
    public GameObject result; 
    public GameObject click;


    [SerializeField] float ropeMoveSpeed;
    [SerializeField] float logoRotateTime;
    [SerializeField] int logoRotatePerSec;
    [SerializeField] int rankAnimStartCount;
    [SerializeField] float rankChangeSpeed;
    [SerializeField] float clickMoveSpeed;


    // logoRotateTimeメモ
    // 停止タイミングはscaleが1.0を超えたタイミングで回転・拡大が止まるので
    // 中途半端な秒数だと角度がズレます。 おススメは0.6か1。
    // 単位は"秒"。ややこしいね。

    struct phaseMode
    {
        public bool ready;
        public bool isPlay;
        public bool isEnd;
    };

    enum phaseName{
        FADEIN_ROPE,
        ROTATE_LOGO,
        FADEIN_RESULT,
        FADEIN_RANK,
        FADEIN_CLICKNEXT,
        MAX
    };

    phaseMode[] phase = new phaseMode[(int)phaseName.MAX];
    [SerializeField] int[] fadeFrame = new int[(int)phaseName.MAX];
    void Start()
    {
        //Application.targetFrameRate = 60;
        // 各オブジェクトをフレーム外に移動させる
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

        defaultPos = air.transform.localPosition;
        defaultPos.x -= 2000f;
        air.transform.localPosition = defaultPos;

        defaultPos = score.transform.localPosition;
        defaultPos.x -= 2000f;
        score.transform.localPosition = defaultPos;

        rankimg.transform.localScale = new Vector3(2.5f, 2.5f, 0.0f);

        defaultPos = click.transform.localPosition;
        defaultPos.y -= 100f;
        click.transform.localPosition = defaultPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (StageManager.instance.isClear == true) { isEvent = true; phase[0].ready = true; }
        if (Input.GetKeyDown(KeyCode.K)) { isEvent = true; phase[0].ready = true; }
        if (canEvent == true)
        {
            int socre = ScoreManager.GetScore();
            float time = ScoreManager.GetAir();
            ScoreManager.RankIndex rank = ScoreManager.GetRank();
            //ここに書く
            transform.GetChild(0).gameObject.SetActive(true);

            // ここから各処理
            if (phase[0].ready && !phase[0].isEnd)
            {
                if (ropeUpper.transform.localPosition.x >= 0.0f) moveNextPhase(0);
                else
                {
                    ropeUpper.transform.position += new Vector3(ropeMoveSpeed * Time.deltaTime, 0f, 0f);
                    ropeRight.transform.position += new Vector3(-ropeMoveSpeed * Time.deltaTime, 0f, 0f);
                    clear.transform.position += new Vector3(ropeMoveSpeed * Time.deltaTime, 0f, 0f);
                }
            }

            if (phase[(int)phaseName.ROTATE_LOGO].ready && !phase[(int)phaseName.ROTATE_LOGO].isEnd)
            {
                
                if (logo.transform.localScale.x <= 1.0f)
                {
                    float rotateTime = 0.2f / ((float)logoRotateTime);
                    logo.transform.Rotate(0f, 0f, -360.0f * logoRotatePerSec * Time.deltaTime);
                    logo.transform.localScale += new Vector3(rotateTime * Time.deltaTime, rotateTime * Time.deltaTime, 0.0f);
                }
                else
                {
                    // ここにいい感じの処理かければもっと綺麗になる
                        moveNextPhase((int)phaseName.ROTATE_LOGO);
                }
            }

            if (phase[(int)phaseName.FADEIN_RESULT].ready && !phase[(int)phaseName.FADEIN_RESULT].isEnd)
            {
                if (result.transform.localPosition.x >= -598.0f) moveNextPhase((int)phaseName.FADEIN_RESULT);
                else
                {
                    result.transform.position += new Vector3(ropeMoveSpeed * Time.deltaTime, 0f, 0f);
                    air.transform.position += new Vector3(ropeMoveSpeed * Time.deltaTime, 0f, 0f);
                    score.transform.position += new Vector3(ropeMoveSpeed * Time.deltaTime, 0f, 0f);
                }
            }

            if (phase[(int)phaseName.FADEIN_RANK].ready && !phase[(int)phaseName.FADEIN_RANK].isEnd)
            {
                if (rankAnimStartCount != 0) rankAnimStartCount--;
                else
                {
                    rankimg.SetActive(true);
                    ranktxt.SetActive(true);
                    if (rankimg.transform.localScale.x <= 1.0f) moveNextPhase((int)phaseName.FADEIN_RANK);
                    else
                    {
                        rankimg.transform.localScale -= new Vector3(rankChangeSpeed * Time.deltaTime, rankChangeSpeed * Time.deltaTime, 0.0f);
                    }
                }
            }
            
            if (phase[(int)phaseName.FADEIN_CLICKNEXT].ready && !phase[(int)phaseName.FADEIN_CLICKNEXT].isEnd)
            {

                if (click.transform.localPosition.y <= -480.0f)
                {
                    click.transform.position += new Vector3(0f, clickMoveSpeed * Time.deltaTime, 0f);
                }
                else
                {
                   // 終了処理
                   //  moveNextPhase((int)phaseName.FADEIN_CLICKNEXT);
                }
            }
        }
    }

    void moveNextPhase(int index)
    {
        if (index < (int)phaseName.MAX - 1)
        {
            phase[index].isEnd = true;
            phase[index + 1].ready = true;
           
        }
        else
        {
            phase[index].isEnd = true;
        }
        return;
    }
}
