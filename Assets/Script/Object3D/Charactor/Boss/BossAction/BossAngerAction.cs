using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAngerAction : BossAction {

    float timer = 0;

    public override void Init(Boss b) {
        base.Init(b);
        boss.animator.SetBool("isAnger", true);
        boss.isDamage = false;
    }

    public override void Action() {
        timer += Time.fixedDeltaTime;
        if (timer > 1) {
            boss.SetAction<BossIdleAction>();
            boss.animator.SetBool("isAnger", false);
        }
    }
}
