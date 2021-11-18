using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleAction : BossAction {
    private enum BossActionIndex {
        SHOTGUN,
        ROTATE_SHOT,
        MAX
    }

    public override void Action() {
        int rand = Random.Range(0, 100);

        if (rand >= 0 && rand < 50) {
            boss.SetAction<BossShotGunAction>();
        }else if(rand >= 50 && rand < 100) {
            boss.SetAction<BossRotateShotAction>();
        }

    }
}
