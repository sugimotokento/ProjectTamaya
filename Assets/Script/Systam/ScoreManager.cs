using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    //ゴールでスコアを設定、ゴールイベントでスコアを表示

    static public ScoreManager instance;
    public enum RankIndex {
        RANK_C,
        RANK_B,
        RANK_A,
        RANK_S,
        RANK_LEGEND,
        RANK_MAX
    }


    private void Awake() {
        instance = this;
    }

    static private float air = -1;
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
        int findScore = 50000;
        int damageScore = 20000;
        int healScore = 20000;
        int sumakiScore = 0;
        int noReTryScore = 10000;

        return findScore + damageScore + healScore + sumakiScore + noReTryScore;
    }

    static public RankIndex GetRank() {
        RankIndex rank = RankIndex.RANK_C;
        int score = GetScore();

        if (score >= 100000) {
            // rank = RankIndex.RANK_LEGEND;
            rank = RankIndex.RANK_S;

        } else if (score >= 70000) {
            rank = RankIndex.RANK_S;

        }else if (score >= 40000) {
            rank = RankIndex.RANK_A;

        }else if (score >= 10000) {
            rank = RankIndex.RANK_B;

        }else if (score >= 0) {
            rank = RankIndex.RANK_C;
        }

        return rank;
    }
}
