using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageAction : BossAction {
    float timer;

    public override void Init(Boss b) {
        base.Init(b);
        boss.isDamage = false;
        boss.animator.SetBool("isDamage", true);
    }

    public override void Action() {
        timer += Time.fixedDeltaTime;
        if (timer > 1.5f) {
            boss.SetAction<BossIdleAction>();
            boss.animator.SetBool("isDamage", false);
        }
    }

}