using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearEvent : GameEvent {
    // Update is called once per frame
    void Update() {
        if (StageManager.instance.isClear == true) {
            isEvent = true;
        }
        if (canEvent == true) {
            int socre = ScoreManager.GetScore();
            float time = ScoreManager.GetAir();
            ScoreManager.RankIndex rank = ScoreManager.GetRank();
            //Ç±Ç±Ç…èëÇ≠
        }
    }



}
