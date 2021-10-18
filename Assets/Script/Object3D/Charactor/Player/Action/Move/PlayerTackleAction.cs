using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTackleAction : PlayerMoveAction {
    public Vector3 mousePos;
    private float timer = 0;
    private float tackeTime = 0.5f;

    Vector3 direction;

    public override void Init(Player p) {
        base.Init(p);
        mousePos = GetWorldMousePos();
        accelerationBaseSpeed *= 1.5f;

        direction = mousePos - player.transform.position;
        player.moveSpeed = direction.normalized * accelerationBaseSpeed;
    }

    public override void Action() {
        Move();

        timer += Time.fixedDeltaTime;
        if (timer > 0.5f) {
            player.ChangeAction<PlayerTackleAction, PlayerMoveAction>();
            PlayerMoveAction moveAction = player.GetAction<PlayerMoveAction>();
            moveAction.SetBrake();
        }
    }

    public override void CollisionEnter(Collision collision) {
        //壁とかに当たったら通常移動になる
        player.ChangeAction<PlayerTackleAction, PlayerMoveAction>();
        PlayerMoveAction moveAction = player.GetAction<PlayerMoveAction>();
        moveAction.SetBrake();

    }
    public override void TriggerEnter(Collider collider) {
        if (collider.CompareTag("Enemy")) {
            player.ChangeAction<PlayerTackleAction, PlayerGuruguruAction>();
            PlayerGuruguruAction guruguruAction = player.GetAction<PlayerGuruguruAction>();
            guruguruAction.SetTartegt(collider.gameObject);
        }
    }

    protected override void Move() {
        //加速して移動する
      //  player.moveSpeed += direction.normalized * accelerationBaseSpeed * Time.fixedDeltaTime;
        player.transform.position +=player.moveSpeed * Time.fixedDeltaTime;

        //燃料を減らす
        player.fuel.Use(); 

        
    }
}
