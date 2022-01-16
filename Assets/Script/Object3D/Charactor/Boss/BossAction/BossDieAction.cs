using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDieAction : BossAction {

    public bool isDieAnimationEnd = false;
    private float dieAnimationTimer = 0;


    public override void Init(Boss b) {
        base.Init(b);
        boss.sound.Play(BossSound.SoundIndex.KILL);
    }
    public override void Action() {
        boss.animator.SetBool("isDie", true);
        boss.animator.SetBool("isIdle", false);
        boss.animator.SetBool("isShot", false);
        boss.animator.SetBool("isDamage", false);
        boss.animator.SetBool("isAnger", false);


        dieAnimationTimer += Time.fixedDeltaTime;
        if (dieAnimationTimer > 4) {
            isDieAnimationEnd = true;
        }
    }
}
