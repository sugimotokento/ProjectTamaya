using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDieAction : PlayerAction {
    public override void Action() {
        player.animator.SetBool("isMove", false);
        player.animator.SetBool("isCharge", false);
        player.animator.SetBool("isTacle", false);
        player.animator.SetBool("isDown", true);
        player.animator.SetBool("isSpin", false);
        player.animator.SetBool("isIai", false);
        player.animator.SetBool("isGoal", false);
    }
}
