using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageAction : BossAction {
    float timer;

    public override void Init(Boss b) {
        base.Init(b);
        boss.isDamage = false;
        boss.animator.SetBool("isDamage", true);

        boss.sound.Play(BossSound.SoundIndex.DAMAGE);
    }

    public override void Action() {

        timer += Time.fixedDeltaTime;
        if (timer > 1.5f) {
            boss.SetAction<BossIdleAction>();
            boss.animator.SetBool("isDamage", false);
        }

    }

    private void SetAlpha(float a) {
        for (int i = 0; i < boss.renderer.Length; ++i) {
            for (int j = 0; j < boss.renderer[i].materials.Length; ++j) {
                boss.renderer[i].materials[j].color = new Color(1, 1, 1, a);
            }
        }
    }
}