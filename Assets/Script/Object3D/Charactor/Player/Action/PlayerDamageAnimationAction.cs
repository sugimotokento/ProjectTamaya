using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageAnimationAction : PlayerAction {
    float timer = 0;

    public override void Action() {
        timer += Time.deltaTime;
        if (player.hP.GetIsDamageInterval() == true) {
            if (Mathf.Sin(timer * 50) > 0) {
                SetAlpha(0);
            } else {
                SetAlpha(1);
            }
        } else {
            SetAlpha(1);
        }

       
    }

    private void SetAlpha(float a) {
        for (int i = 0; i < player.rendere.Length; ++i) {
            for (int j = 0; j < player.rendere[i].materials.Length; ++j) {
                player.rendere[i].materials[j].SetFloat("_Alpha", a);
            }
        }
    }

}
