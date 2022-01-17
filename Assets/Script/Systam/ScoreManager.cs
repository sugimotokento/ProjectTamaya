using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {
    //ゴールでスコアを設定、ゴールイベントでスコアを表示

    public enum RankIndex {
        RANK_C,
        RANK_B,
        RANK_A,
        RANK_S,
        RANK_SS,
        RANK_LEGEND,
        RANK_MAX
    }


    private void Awake() {

    }

    [HideInInspector] static public float air = -1;
    [HideInInspector] static public int findNum = 0;
    [HideInInspector] static public int damageNum = 0;
    [HideInInspector] static public int healNum = 0;
    [HideInInspector] static public int sumakiNum = 0;
    [HideInInspector] static public int retryNum = 0;
    [HideInInspector] static public int[] stageScore = new int[5];
    [HideInInspector] static public RankIndex[] stageRank = new RankIndex[5];


    // Start is called before the first frame update
    void Start() {
        air = -1;
        findNum = 0;
        damageNum = 0;
        healNum = 0;
        sumakiNum = 0;
        retryNum = 0;
    }

    // Update is called once per frame
    void Update() {

    }

    static public void SetAir(float score) {

        air = score;
    }
    static public float GetAir() {
        return air;
    }
    static public int GetScore() {
        int findScore = 50000 - findNum * 5000;
        int damageScore = 20000 - damageNum * 1000;
        int healScore = 20000 - healNum * 2500;
        int sumakiScore = 0 + sumakiNum * 750;
        int noReTryScore = 10000 - retryNum * 10000;
        int airScore = (int)air * 10;

        findScore    = Mathf.Max(0, findScore);
        damageScore  = Mathf.Max(0, damageScore);
        healScore    = Mathf.Max(0, healScore);
        sumakiScore  = Mathf.Max(0, sumakiScore);
        noReTryScore = Mathf.Max(0, noReTryScore);
        airScore     = Mathf.Max(0, airScore);

        int score=findScore +damageScore + healScore + sumakiScore + noReTryScore + airScore;

        string name = SceneManager.GetActiveScene().name;
        if (name == "Stage1") {
            stageScore[0] = score;
        }
        if (name == "Stage2") {
            stageScore[1] = score;
        }
        if (name == "Stage3") {
            stageScore[2] = score;
        }
        if (name == "Stage4") {
            stageScore[3] = score;
        }
        if (name == "Stage5") {
            stageScore[4] = score;
        }

        return score;
    }

    static public RankIndex GetRank() {
        RankIndex rank = RankIndex.RANK_C;
        int score = GetScore();

        if (score >= 100000) {
            rank = RankIndex.RANK_SS;

            if (air >= LegendAirBorder()) {
                rank = RankIndex.RANK_LEGEND;
            }

        } else if (score >= 70000) {
            rank = RankIndex.RANK_S;

        } else if (score >= 40000) {
            rank = RankIndex.RANK_A;

        } else if (score >= 10000) {
            rank = RankIndex.RANK_B;

        } else if (score >= 0) {
            rank = RankIndex.RANK_C;
        }


        string name = SceneManager.GetActiveScene().name;
        if (name == "Stage1") {
            stageRank[0] = rank;
        }
        if (name == "Stage2") {
            stageRank[1] = rank;
        }
        if (name == "Stage3") {
            stageRank[2] = rank;
        }
        if (name == "Stage4") {
            stageRank[3] = rank;
        }
        if (name == "Stage5") {
            stageRank[4] = rank;
        }


        return rank;
    }
    static public RankIndex GetAllRank() {
        RankIndex rank = RankIndex.RANK_C;
        int score=0;

        score = GetAllScore();
        bool isLegend = true;
        for (int i = 0; i < 5; ++i) {
            if (stageRank[i] != RankIndex.RANK_LEGEND) {
                isLegend = false;
                break;
            }
        }


        //ランクを決定する
        if (score >= 100000*5) {
            rank = RankIndex.RANK_SS;

            if (isLegend ==true) {
                rank = RankIndex.RANK_LEGEND;
            }

        } else if (score >= 70000 * 5) {
            rank = RankIndex.RANK_S;

        } else if (score >= 40000 * 5) {
            rank = RankIndex.RANK_A;

        } else if (score >= 10000 * 5) {
            rank = RankIndex.RANK_B;

        } else if (score >= 0) {
            rank = RankIndex.RANK_C;
        }



        return rank;
    }
    static public int GetAllScore() {
        int score = 0;

        //今までのスコアの合計
        for (int i = 0; i < 5; ++i) {
            score += stageScore[i];
        }

        return score;
    }

    static float LegendAirBorder() {
        string name = SceneManager.GetActiveScene().name;
        if (name == "Stage1") {
            return 255;
        }
        if (name == "Stage2") {
            return 300;
        }
        if (name == "Stage3") {
            return 200;
        }
        if (name == "Stage4") {
            return 250;
        }
        if (name == "Stage5") {
            return 330;
        }

        return 0;
    }
}
