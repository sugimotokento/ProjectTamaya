using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageAnimationAction : PlayerAction {
    float timer = 0;

    public override void Action() {
        timer += Time.deltaTime;
        if (player.hP.GetIsDamageInterval() == true) {
            if (Mathf.Sin(timer * 50) > 0) {
                player.visual.SetActive(false);
            } else {
                player.visual.SetActive(true);
            }
        } else {
            player.visual.SetActive(true);
        }
    }

}
