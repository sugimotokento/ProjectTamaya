using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotateShotAction : BossAction {
    private const float SHOT_TIME = 20;
    private const float SHOT_INTERVAL = 0.15f;
    private const float ROTATE_SPEED = 40;
    private LineRenderer line;

    private float shotTimer = 0;
    private float shotIntervalTimer = 0;
    private bool canShot = false;

    public override void Init(Boss b) {
        base.Init(b);

        boss.line.SetActive(true);
        line = boss.line.GetComponent<LineRenderer>();
        line.positionCount = 2;

        line.startWidth = 0.1f;
        line.endWidth = 0.1f;

        line.materials[0].color = Color.blue;
        line.materials[0].SetColor("_EmissionColor", Color.blue * 20);
    }


    public override void Action() {
        LockAtPlayer();
        Shot();
        End();
    }


    private void LockAtPlayer() {
        if (canShot == false) {
            Player player = StageManager.instance.player;

            //いろいろ計算
            Vector3 dist = boss.gameObject.transform.position - player.gameObject.transform.position;
            Vector3 cross = Vector3.Cross(dist.normalized, boss.gameObject.transform.right);
            float angle = Vector3.Dot(dist.normalized, boss.gameObject.transform.right);
            angle = Mathf.Acos(angle) / 3.14f * 180;

            //プレイヤーの方に回転
            if (cross.z > 0) {
                boss.transform.Rotate(Vector3.forward * 200 * Time.fixedDeltaTime);
            } else {
                boss.transform.Rotate(Vector3.forward * -200 * Time.fixedDeltaTime);
            }
            //プレイヤーとの距離とボスの視線の角度が一定以内
            if (angle > 175) {
                canShot = true;
                boss.line.gameObject.SetActive(false);
            }


            line.SetPosition(0, boss.gameObject.transform.position);
            line.SetPosition(1, boss.gameObject.transform.position+boss.gameObject.transform.right * 100);
        }
    }
    private void Shot() {
        if (canShot == true) {
            shotIntervalTimer += Time.fixedDeltaTime;
            shotTimer += Time.fixedDeltaTime;

            if (shotIntervalTimer > SHOT_INTERVAL) {
                shotIntervalTimer = 0;

                //ボスの向いている角度を求める
                float angle = Mathf.Atan2(boss.gameObject.transform.right.y, boss.gameObject.transform.right.x);
                angle = angle / 3.14f * 180;

                //5秒後に消える弾を生成
                GameObject bullet = Instantiate(boss.bullet, boss.gameObject.transform.position, Quaternion.Euler(0, 0, angle - 90));
                Destroy(bullet, 5);


            }
            boss.transform.Rotate(Vector3.forward * ROTATE_SPEED * Time.fixedDeltaTime);

        }
    }

    private void End() {
        if (shotTimer > SHOT_TIME) {
            boss.SetAction<BossIdleAction>();
        }
    }
}
