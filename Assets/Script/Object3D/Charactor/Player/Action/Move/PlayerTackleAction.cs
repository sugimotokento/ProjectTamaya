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
            player.moveSpeed *= 0.1f;
        }
    }

    public override void CollisionEnter(Collision collision) {
        //•Ç‚Æ‚©‚É“–‚½‚Á‚½‚ç’ÊíˆÚ“®‚É‚È‚é
        player.ChangeAction<PlayerTackleAction, PlayerMoveAction>();
        player.moveSpeed *= 0.1f;

    }
    public override void TriggerEnter(Collider collider) {
        if (collider.CompareTag("Enemy")) {
            player.ChangeAction<PlayerTackleAction, PlayerGuruguruAction>();
            PlayerGuruguruAction guruguruAction = player.GetAction<PlayerGuruguruAction>();
            guruguruAction.SetTartegt(collider.gameObject);
            player.moveSpeed *= 0.1f;
        }
    }

    protected override void Move() {
        //‰Á‘¬‚µ‚ÄˆÚ“®‚·‚é
      //  player.moveSpeed += direction.normalized * accelerationBaseSpeed * Time.fixedDeltaTime;
        player.transform.position +=player.moveSpeed * Time.fixedDeltaTime;

        //”R—¿‚ğŒ¸‚ç‚·
        player.fuel.Use(); 

        
    }
}
