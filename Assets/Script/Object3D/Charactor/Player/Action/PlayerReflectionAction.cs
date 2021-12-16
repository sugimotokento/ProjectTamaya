using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReflectionAction : PlayerAction {
    private bool isReflection = false;

    public override void Init(Player p) {
        base.Init(p);
    }

    public override void Action() {
        if (isReflection == false) {
            player.animator.SetBool("isSpin", false);
        }
        isReflection = false;
    }

    public override void CollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Stage")) {
            Ray rayUp = new Ray(player.transform.position, Vector3.up);
            Ray rayDown = new Ray(player.transform.position, Vector3.down);
            Ray rayLeft = new Ray(player.transform.position, Vector3.left);
            Ray rayRight = new Ray(player.transform.position, Vector3.right);

            RaycastHit hit;

            if (Physics.Raycast(rayUp, out hit, 0.6f)) {
                Reflection(Vector3.down, collision);
            }
            if (Physics.Raycast(rayDown, out hit, 0.6f)) {
                Reflection(Vector3.up, collision);
            }
            if (Physics.Raycast(rayLeft, out hit, 0.6f)) {
                Reflection(Vector3.right, collision);
            }
            if (Physics.Raycast(rayRight, out hit, 0.6f)) {
                Reflection(Vector3.left, collision);
            }

        } else if (collision.gameObject.CompareTag("StageEX")) {
            Reflection(collision.gameObject.transform.up, collision);
            
        }
    }

    private void Reflection(Vector3 normal, Collision collision) {
        PlayerMoveAction moveAction = player.GetAction<PlayerMoveAction>();

        // 反射ベクトルを計算する
        Vector3 n = normal;
        float h = Mathf.Abs((Vector3.Dot(player.moveSpeed, n)));
        Vector3 r = player.moveSpeed + 2 * n * h;
        player.moveSpeed = r;
        player.moveSpeed *= 0.7f;

        isReflection = true;
        player.animator.SetBool("isSpin", true);


        string name = collision.gameObject.name;
        if (name.Length >= 7) {
            name = name.Substring(0, 7);
            if (name == "Cushion") {
                player.sound.PlayShot(PlayerSound.SoundIndex.HIT_MAT);

            } else {
                player.sound.PlayShot(PlayerSound.SoundIndex.BOUND);
            }
        }
    }

}
