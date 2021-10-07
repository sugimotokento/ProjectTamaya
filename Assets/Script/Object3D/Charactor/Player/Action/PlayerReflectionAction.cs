using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReflectionAction : PlayerAction {

    public override void Init(Player p) {
        base.Init(p);

    }

    public override void Action() {
        Ray rayUp = new Ray(player.transform.position, Vector3.up);
        Ray rayDown = new Ray(player.transform.position, Vector3.down);
        Ray rayLeft = new Ray(player.transform.position, Vector3.left);
        Ray rayRight = new Ray(player.transform.position, Vector3.right);

        RaycastHit hit;

        if (Physics.Raycast(rayUp, out hit, 0.6f)) {
            Reflection(Vector3.down);
        }
        if (Physics.Raycast(rayDown, out hit, 0.6f)) {
            Reflection(Vector3.up);
        }
        if (Physics.Raycast(rayLeft, out hit, 0.6f)) {
            Reflection(Vector3.right);
        }
        if (Physics.Raycast(rayRight, out hit, 0.6f)) {
            Reflection(Vector3.left);
        }
    }

    private void Reflection(Vector3 normal) {
        PlayerMoveAction moveAction = player.GetAction<PlayerMoveAction>();

        // 反射ベクトルを計算する
        Vector3 n = normal;
        float h = Mathf.Abs(Mathf.Cos(90/180*3.14f));
        Vector3 r = moveAction.moveSpeed + 2 * n * h;
        moveAction.moveSpeed = r*1.01f;
    }

}
