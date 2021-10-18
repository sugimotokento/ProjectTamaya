using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIaiAction : PlayerMoveAction {
    GameObject hitEnemy;

    LineRenderer line;

    Vector3 attackPos;
    Vector3 moveSpeedBuffer;

    bool isCharge = true;
    bool isEnemyHit = false;
    bool isEnd = false;

    public override void Init(Player p) {
        base.Init(p);

       
        line = player.line[0].GetComponent<LineRenderer>();
        line.positionCount = 2;

        line.materials[0].color = Color.green;
        line.materials[0].SetColor("_EmissionColor", Color.green);
    }

    public override void CollisionEnter(Collision collision) {
        if (isCharge == false) {
            isEnd = true;
        }
    }

    public override void TriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Enemy")) {
            hitEnemy = collider.gameObject;
            isEnemyHit = true;
        }
    }

    public override void Action() {
        Iai();
        End();
    }

    private void Iai() {
        player.transform.position += player.moveSpeed * Time.fixedDeltaTime;

        if (isCharge == true) {
            //エフェクト生成
            Instantiate(player.iaiEffect, player.transform.position, Quaternion.identity);

            //線の座標設定
            line.SetPosition(0, player.transform.position);
            line.SetPosition(1, GetWorldMousePos());
            player.line[0].SetActive(true);

            //居合チャージ終了判定
            if (Input.GetMouseButton(1) == false) {
                isCharge = false;
                attackPos = GetWorldMousePos();
                moveSpeedBuffer = player.moveSpeed;

                player.moveSpeed = (attackPos - player.transform.position).normalized * accelerationBaseSpeed*3;
            }
        } else {
            //目標地点に到達
            if ((attackPos - player.transform.position).magnitude < 0.2f) {
                isEnd = true;
            }
        }
    }

    private void End() {
        if (isEnd == true) {
            //移動速度を戻す
            player.moveSpeed *= 0.1f;

            //線を消す
            player.line[0].SetActive(false);


            if (isEnemyHit == true) {
                //敵に当たってた
                player.ChangeAction<PlayerIaiAction, PlayerGuruguruAction>();
                PlayerGuruguruAction guruguruAction = player.GetAction<PlayerGuruguruAction>();
                guruguruAction.SetTartegt(hitEnemy);
            } else {
                //移動に戻す
                player.ChangeAction<PlayerIaiAction, PlayerMoveAction>();

            }
        }

    }

}