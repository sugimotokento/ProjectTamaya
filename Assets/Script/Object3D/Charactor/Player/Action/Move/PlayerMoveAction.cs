using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAction : PlayerAction {
    public Vector3 moveSpeed;
    protected float ACCELERATION_SPEED_BASE = 10;

    public override void Init(Player p) {
        base.Init(p);
    }

    public override void UpdateAction() {
        if (Input.GetMouseButtonDown(1)) {
            PlayerMoveAction moveAction = player.GetAction<PlayerMoveAction>();
            moveAction = new PlayerTackleAction();
        }


        if (Input.GetMouseButtonDown(0) && player.fuel.canUse==true) {
            player.audioSource.PlayOneShot(player.sound1);
        }
    }

    public override void Action() {
        
        if (Input.GetMouseButton(0) && player.fuel.canUse == true) {
            //マウスカーソルの3D座標とプレイヤーの座標の距離を取って加速する向きを計算
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            
            Vector3 dist = mousePos - player.transform.position;
            dist.z = 0;
            moveSpeed += dist.normalized * ACCELERATION_SPEED_BASE * Time.fixedDeltaTime;

            player.fuel.UseFuel();
        }

        player.transform.position += moveSpeed * Time.fixedDeltaTime;



    }
}
