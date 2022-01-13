using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClearEvent : GameEvent {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (StageManager.instance.boss != null ) {
            Boss boss = StageManager.instance.boss;
            if (boss.CheckAction<BossDieAction>() == true && boss.GetAction<BossDieAction>().isDieAnimationEnd == true) {
                isEvent = true;
            }
        }

        if (canEvent == true) {
            if (isEvent == true) {
                
            }
        }
    }
}
