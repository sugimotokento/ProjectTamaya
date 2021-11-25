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
    }

    public override void Action() {
        int rand = Random.Range(0, 100);

        if (rand >= 0 && rand < 33) {
            boss.SetAction<BossShotGunAction>();

        }else if(rand >= 33 && rand < 66) {
            boss.SetAction<BossRotateShotAction>();

        }else if (rand >= 66) {
            boss.SetAction<BossLandmineAction>();
        }

    }
}
