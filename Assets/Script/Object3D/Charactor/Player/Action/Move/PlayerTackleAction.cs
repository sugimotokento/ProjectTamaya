using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTackleAction : PlayerMoveAction {
    public Vector3 mousePos;
    private float timer = 0;
    private float tackeTime = 0.5f;


    public override void Init(Player p) {
        base.Init(p);

    }

    public override void Action() {
        Move();

        timer += Time.fixedDeltaTime;
        if (timer > 0.5f) {
            player.ChangeAction<PlayerTackleAction, PlayerMoveAction>();
            
        }
    }

    public override void CollisionEnter(Collision collision) {
        //�ǂƂ��ɓ���������ʏ�ړ��ɂȂ�
        player.ChangeAction<PlayerTackleAction, PlayerMoveAction>();
        
    }


    protected override void Move() {
        Vector3 dist = mousePos - player.transform.position;
        dist.z = 0;

        //�������Ĉړ�����
        player.moveSpeed += dist.normalized * accelerationBaseSpeed * Time.fixedDeltaTime;
        player.transform.position += (player.moveSpeed * (1 + timer * 2)) * Time.fixedDeltaTime;

        //�R�������炷
        player.fuel.Use(); 

        
    }
}
