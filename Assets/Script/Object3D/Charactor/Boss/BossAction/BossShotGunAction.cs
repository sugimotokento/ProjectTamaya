using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotGunAction : BossAction {
    private const float SHOT_INTERVAL = 0.2f;
    private const float SHOT_TIME = 1.5f;

    private LineRenderer line;

    private int shotCount = 0;

    private float shotIntervalTimer = 0;
    private float shotTimer = 0;

    private bool isShot = false;
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

        boss.animator.SetBool("isShot", true);

    }

    public override void Action() {
        LockAtPlayer();
        Shot();
        End();

        if (boss.isDamage == true) {
            if (boss.hp == 3) {
                boss.SetAction<BossAngerAction>();
            } else {
                boss.SetAction<BossDamageAction>();
            }
        }
    }

    private void LockAtPlayer() {
        Player player = StageManager.instance.player;

        //いろいろ計算
        Vector3 dist = boss.gameObject.transform.position - player.gameObject.transform.position;
        Vector3 cross = Vector3.Cross(dist.normalized, boss.gameObject.transform.right);
        float angle = Vector3.Dot(dist.normalized, boss.gameObject.transform.right);
        angle = Mathf.Acos(angle) / 3.14f * 180;

        //プレイヤーの方に回転
        if (isShot == false) {
            if (angle < 177) {
                if (cross.z > 0) {
                    boss.transform.Rotate(Vector3.forward * 150 * Time.fixedDeltaTime);
                } else {
                    boss.transform.Rotate(Vector3.forward * -150 * Time.fixedDeltaTime);
                }
            } 
            if (angle > 165) {
                canShot = true;
            } else {
                canShot = false;
            }
        }
        
        line.SetPosition(0, boss.mazzle.transform.position);
        line.SetPosition(1, boss.mazzle.transform.position + boss.gameObject.transform.right * 100);

    }
    private void Shot() {
        const float SHOT_RANGE = 35;
        const float LOOP_MAX = 5;

        shotIntervalTimer += Time.fixedDeltaTime;
        shotTimer += Time.fixedDeltaTime;

        if (canShot == true) {
            if (shotIntervalTimer > SHOT_INTERVAL) {
                shotIntervalTimer = 0;
                isShot = true;
                boss.sound.Play(BossSound.SoundIndex.SHOT);
                //弾生成
                for (int i = 0; i < LOOP_MAX; ++i) {
                    //ボスの向いている角度を求める
                    float angle = Mathf.Atan2(boss.gameObject.transform.right.y, boss.gameObject.transform.right.x);
                    angle = angle / 3.14f * 180;
                    boss.line.SetActive(false);
                    //各弾の発射角度を計算
                    float shotAngle = angle - SHOT_RANGE / 2 + (SHOT_RANGE / LOOP_MAX * i);
                    //5秒後に消える弾を生成
                    Vector3 pos = boss.mazzle.transform.position;
                    pos.z = 0;
                    Destroy(Instantiate(boss.bullet,pos, Quaternion.Euler(0, 0, shotAngle - 90)), 5);
                }
            }
        }
        //レーザー描画
        if (shotTimer > SHOT_TIME * 0.5f) {
            boss.line.SetActive(true);
        }

        //一定時間撃ったらプレイヤーの方を向く処理に移行
        if (shotTimer > SHOT_TIME) {
            shotIntervalTimer = 0;
            shotTimer = 0;
            shotCount++;

            isShot = false;
        }

    }
    private void End() {
        if (shotCount >= 3) {
            boss.SetAction<BossIdleAction>();
            boss.animator.SetBool("isShot", false);

        }
    }
}
