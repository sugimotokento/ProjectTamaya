using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDieAction : PlayerAction {
    public override void Init(Player p) {
        base.Init(p);

       
    }

    public override void Action() {
        player.animator.SetBool("isMove", false);
        player.animator.SetBool("isCharge", false);
        player.animator.SetBool("isTacle", false);
        player.animator.SetBool("isDown", true);
        player.animator.SetBool("isSpin", false);
        player.animator.SetBool("isIai", false);
        player.animator.SetBool("isGoal", false);

        for (int i = 0; i < player.rendere.Length; ++i) {
            for (int j = 0; j < player.rendere[i].materials.Length; ++j) {
                player.rendere[i].materials[j].SetFloat("_Alpha", 1);
            }
        }
        
    }
}
