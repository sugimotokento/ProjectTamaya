using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartIdleAction : BossAction {

    float timer = 0;
    public override void Action() {
        Player player = StageManager.instance.player;

        //いろいろ計算
        Vector3 dist = boss.gameObject.transform.position - player.gameObject.transform.position;
        Vector3 cross = Vector3.Cross(dist.normalized, boss.gameObject.transform.right);
        float angle = Vector3.Dot(dist.normalized, boss.gameObject.transform.right);
        angle = Mathf.Acos(angle) / 3.14f * 180;

        //プレイヤーの方に回転
        if (angle < 180) {
            if (cross.z > 0) {
                boss.transform.Rotate(Vector3.forward * 150 * Time.fixedDeltaTime*(1- Mathf.Abs(Vector3.Dot(dist.normalized, boss.gameObject.transform.right))));
            } else {
                boss.transform.Rotate(Vector3.forward * -150 * Time.fixedDeltaTime);
            }
        }



        boss.hpUI.SetFillAmount(timer / 1.5f);

        timer += Time.deltaTime;

        if (timer > 2) {
            boss.hpUI.isBattle = true;
            boss.SetAction<BossIdleAction>();
        }
    }
}
