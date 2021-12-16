using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTackleAction : PlayerMoveAction {
    public Vector3 mousePos;
    private float timer = 0;
    private float tackeTime = 0.5f;
    private float afterimageTimer = 0;

    Vector3 direction;

    public override void Init(Player p) {
        base.Init(p);
        mousePos = GetWorldMousePos();
        accelerationBaseSpeed *= 1.5f;

        direction = mousePos - player.transform.position;
        player.moveSpeed = direction.normalized * accelerationBaseSpeed;

        player.animator.SetBool("isTacle", true);
        player.animator.SetBool("isMove", false);

        player.sound.PlayShot(PlayerSound.SoundIndex.TACKLE);
    }

    public override void Action() {
        Move();

        timer += Time.fixedDeltaTime;
        if (timer > 0.5f) {
            player.ChangeAction<PlayerTackleAction, PlayerMoveAction>();
            player.moveSpeed *= 0.1f;
        }
    }

    public override void CollisionEnter(Collision collision) {
        //壁とかに当たったら通常移動になる
        player.ChangeAction<PlayerTackleAction, PlayerMoveAction>();
        player.moveSpeed *= 0.1f;

    }
    public override void TriggerEnter(Collider collider) {
        if (collider.CompareTag("Enemy")) {
            if (collider.gameObject.GetComponent<Enemy>().isSumaki == false) {
                player.ChangeAction<PlayerTackleAction, PlayerGuruguruAction>();
                PlayerGuruguruAction guruguruAction = player.GetAction<PlayerGuruguruAction>();
                guruguruAction.SetTartegt(collider.gameObject);
                player.moveSpeed *= 0.1f;

                player.sound.PlayShot(PlayerSound.SoundIndex.HIT_TACKLE);
            }
        }
    }

    protected override void Move() {
        //加速して移動する
        player.transform.position +=player.moveSpeed * Time.fixedDeltaTime;

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

        //燃料を減らす
        player.fuel.Use(3);


        //残像
        if (afterimageTimer > 0.02f) {
            GameObject obj = Instantiate(player.afterimage, player.visual.transform.position, player.visual.transform.rotation);
            Destroy(obj, 0.2f);
            afterimageTimer = 0;
        }
        afterimageTimer += Time.fixedDeltaTime;

        if (player.fuel.GetCanUse() == false) {
            player.ChangeAction<PlayerTackleAction, PlayerMoveAction>();
            player.moveSpeed *= 0.1f;
        }
    }
}
