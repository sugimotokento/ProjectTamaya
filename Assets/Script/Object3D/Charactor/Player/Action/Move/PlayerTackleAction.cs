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
        accelerationBaseSpeed *= 5;
    }

    public override void Action() {
        Move();

        timer += Time.fixedDeltaTime;
        if (timer > 0.5f) {
            player.ChangeAction<PlayerTackleAction, PlayerMoveAction>();
            
        }
    }

    public override void CollisionEnter(Collision collision) {
        //•Ç‚Æ‚©‚É“–‚½‚Á‚½‚ç’ÊíˆÚ“®‚É‚È‚é
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

        //‰Á‘¬‚µ‚ÄˆÚ“®‚·‚é
        player.moveSpeed += dist.normalized * accelerationBaseSpeed * Time.fixedDeltaTime;
        player.transform.position +=player.moveSpeed * Time.fixedDeltaTime;

        //”R—¿‚ğŒ¸‚ç‚·
        player.fuel.Use(); 

        
    }
}
