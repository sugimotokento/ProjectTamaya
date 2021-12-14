using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDieAction : BossAction {
    public override void Action() {
        boss.animator.SetBool("isDie", true);
        boss.animator.SetBool("isIdle", false);
        boss.animator.SetBool("isShot", false);
        boss.animator.SetBool("isDamage", false);
        boss.animator.SetBool("isAnger", false);


     
    }
}
