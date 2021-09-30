using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAction : PlayerAction {
    private Vector3 moveSpeed;
    private float ACCELERATION_SPEED = 10;

    public override void Init(Player p) {
        base.Init(p);
    }
    public override void Action() {
        if (Input.GetMouseButton(0)) {
            //マウスカーソルの3D座標とプレイヤーの座標の距離を取って加速する向きを計算
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            
            Vector3 dist = mousePos - player.transform.position;
            dist.z = 0;
            moveSpeed += dist.normalized * ACCELERATION_SPEED * Time.fixedDeltaTime;
        }

        player.transform.position += moveSpeed * Time.fixedDeltaTime;
    }
}
