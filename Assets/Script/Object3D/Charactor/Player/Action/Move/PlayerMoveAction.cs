using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAction : PlayerAction {
    protected float accelerationBaseSpeed = 10;

    private const float CHANGE_ACTION_INTERVAL = 1.4f;
    private const float BOOST_POWER = 30;
    private float rightCrickTimer = 0;
    private bool isLeftMouseDown = false;

    public override void Init(Player p) {
        base.Init(p);
        rightCrickTimer = 0;
    }

    public override void UpdateAction() {
        if (Input.GetMouseButtonDown(0)) {
            isLeftMouseDown = true;
        }
    }

    public override void Action() {
        Move();
        ChangeAction();
    }


    protected virtual void Move() {
        if (isLeftMouseDown==true && player.fuel.canUse == true) {
            player.sound.PlayShot(PlayerSound.SoundIndex.MOVE);

            //マウスカーソルの3D座標とプレイヤーの座標の距離を取って加速する向きを計算
            Vector3 dist = GetWorldMousePos() - player.transform.position;
            dist.z = 0;
            player.moveSpeed += dist.normalized * accelerationBaseSpeed * Time.fixedDeltaTime*BOOST_POWER;

            player.fuel.Use(); //燃料を減らす
        }

        player.transform.position += player.moveSpeed * Time.fixedDeltaTime;
        isLeftMouseDown = false;
    }


    private void ChangeAction() {
        if (Input.GetMouseButton(1)) {
            rightCrickTimer += Time.fixedDeltaTime;

        } else {
            //タックルモード
            if (rightCrickTimer > 0 && rightCrickTimer < CHANGE_ACTION_INTERVAL) {
                Debug.Log("タックルチェンジ");

                //クラスの切り替え
                player.ChangeAction<PlayerMoveAction, PlayerTackleAction>();
                PlayerTackleAction tackleAction = player.GetAction<PlayerTackleAction>();
                tackleAction.mousePos = GetWorldMousePos();

            }else if (rightCrickTimer >= CHANGE_ACTION_INTERVAL) {
                Debug.Log("居合チェンジ");
                //player.ChangeAction<PlayerMoveAction, PlayerIaiAction>();
                //PlayerIaiAction iaiAction = player.GetAction<PlayerIaiAction>();
            }
        }
    }

    
}
