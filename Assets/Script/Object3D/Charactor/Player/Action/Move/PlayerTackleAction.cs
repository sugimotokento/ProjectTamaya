using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTackleAction : PlayerMoveAction {
    public Vector3 mousePos;
    private float timer = 0;
    private float tackeTime = 0.5f;


    public override void Init(Player p) {
        base.Init(p);
        mousePos = GetWorldMousePos();
    }

    public override void Action() {
        Move();

        timer += Time.fixedDeltaTime;
        if (timer > 0.5f) {
            player.ChangeAction<PlayerTackleAction, PlayerMoveAction>();
            
        }
    }

    public override void CollisionEnter(Collision collision) {
        //壁とかに当たったら通常移動になる
        player.ChangeAction<PlayerTackleAction, PlayerMoveAction>();
        
    }
    public override void TriggerEnter(Collider collider) {
        if (collider.CompareTag("Enemy")) {
            player.ChangeAction<PlayerTackleAction, PlayerGuruguruAction>();
            PlayerGuruguruAction guruguruAction = player.GetAction<PlayerGuruguruAction>();
            guruguruAction.SetTartegt(collider.gameObject);
        }
    }


    protected override void Move() {
        Vector3 dist = mousePos - player.transform.position;
        dist.z = 0;

        //加速して移動する
        player.moveSpeed += dist.normalized * accelerationBaseSpeed * Time.fixedDeltaTime;
        player.transform.position += (player.moveSpeed * (1 + timer * 2)) * Time.fixedDeltaTime;

        //燃料を減らす
        player.fuel.Use(); 

        
    }
}
