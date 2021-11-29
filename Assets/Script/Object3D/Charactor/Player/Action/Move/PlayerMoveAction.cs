using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAction : PlayerAction {
    protected float accelerationBaseSpeed;

    private const float CHANGE_ACTION_INTERVAL = 1.4f;
    private float rightCrickTimer = 0;
    private bool isLeftMouseDown = false;

    public override void Init(Player p) {
        base.Init(p);
        rightCrickTimer = 0;
        accelerationBaseSpeed = 10;

        player.animator.SetBool("isTacle", false);
        player.animator.SetBool("isIai", false);
    }

    public override void UpdateAction() {
        if (Input.GetMouseButtonDown(0)) {
            isLeftMouseDown = true;
            player.sound.PlayShot(PlayerSound.SoundIndex.MOVE);
        }
    }

    public override void Action() {
 
        Move();
        ChangeAction();

    }


    protected virtual void Move() {
        if (Input.GetMouseButton(0) && player.fuel.GetCanUse() == true) {

            //マウスカーソルの3D座標とプレイヤーの座標の距離を取って加速する向きを計算
            Vector3 distance = GetWorldMousePos() - player.transform.position;
            distance.z = 0;
            player.moveSpeed += distance.normalized * accelerationBaseSpeed * Time.fixedDeltaTime;
            player.moveSpeed.z = 0;
            player.fuel.Use(); //燃料を減らす
        }
        player.transform.position += player.moveSpeed * Time.fixedDeltaTime;

        //プレイヤーの見た目を回転
        Vector3 dist = player.transform.position - player.positionBuffer;
        Vector3 angle = Vector3.zero;
        angle.z = Mathf.Atan2((dist.y), Mathf.Abs(dist.x)) / 3.14f * 180;
        if (dist.x > 0) {
            angle.y = 0;
        } else {
            angle.y = 180;
        }

        player.visual.transform.rotation = Quaternion.Euler(0.0f, angle.y, angle.z);

        isLeftMouseDown = false;

        //モーション
        dist = player.gameObject.transform.position - player.positionBuffer;
        if (dist.magnitude < 0.02f) {
            player.animator.SetBool("isMove", false);
        } else {
            player.animator.SetBool("isMove", true);
        }
    }


    private void ChangeAction() {
        if (Input.GetMouseButton(1) && player.fuel.GetCanUse() == true) {
            rightCrickTimer += Time.fixedDeltaTime;

            //エフェクト生成
            if (rightCrickTimer > 0.5f) {
                Instantiate(player.iaiEffect, player.transform.position, Quaternion.identity);
            }

            if (rightCrickTimer >= CHANGE_ACTION_INTERVAL) {
                player.ChangeAction<PlayerMoveAction, PlayerIaiAction>();
                player.animator.SetBool("isMove", false);
            }
        } else {
            //タックルモード
            if (rightCrickTimer > 0 && rightCrickTimer < CHANGE_ACTION_INTERVAL) {

                //クラスの切り替え
                player.ChangeAction<PlayerMoveAction, PlayerTackleAction>();
                
                player.animator.SetBool("isMove", false);

            }
        }
    }

}