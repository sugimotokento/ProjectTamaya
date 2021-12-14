using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleAction : BossAction {
    private enum BossActionIndex {
        SHOTGUN,
        ROTATE_SHOT,
        MAX
    }

    public override void Init(Boss b) {
        base.Init(b);
        boss.line.SetActive(false);

        boss.animator.SetBool("isIdle", true);
    }

    public override void Action() {
        int rand = Random.Range(0, 100);
        boss.animator.SetBool("isIdle", false);
        if (rand >= 0 && rand < 33) {
            boss.SetAction<BossShotGunAction>();

        } else if (rand >= 33 && rand < 66) {
            boss.SetAction<BossRotateShotAction>();

        } else if (rand >= 66) {
            boss.SetAction<BossLandmineAction>();
        }


        if (boss.isDamage == true) {
            if (boss.hp == 2) {
                boss.SetAction<BossAngerAction>();
            } else {
                boss.SetAction<BossDamageAction>();
            }
        }

        
    }
}
