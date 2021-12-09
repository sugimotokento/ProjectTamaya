using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    static private float air = -1;
    static private int findNum = 0;
    static private int damageNum = 0;
    static private int healNum = 0;
    static private int sumakiNum = 0;
    static private int retryNum = 0;


    // Start is called before the first frame update
    void Start() {

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

        return findScore + damageScore + healScore + sumakiScore + noReTryScore + airScore;
    }

    static public RankIndex GetRank() {
        RankIndex rank = RankIndex.RANK_C;
        int score = GetScore();

        if (score >= 100000) {
            rank = RankIndex.RANK_SS;

        } else if (score >= 70000) {
            rank = RankIndex.RANK_S;

        } else if (score >= 40000) {
            rank = RankIndex.RANK_A;

        } else if (score >= 10000) {
            rank = RankIndex.RANK_B;

        } else if (score >= 0) {
            rank = RankIndex.RANK_C;
        }

        return rank;
    }
}
