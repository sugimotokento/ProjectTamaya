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
        //ï«Ç∆Ç©Ç…ìñÇΩÇ¡ÇΩÇÁí èÌà⁄ìÆÇ…Ç»ÇÈ
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
        //â¡ë¨ÇµÇƒà⁄ìÆÇ∑ÇÈ
        player.transform.position +=player.moveSpeed * Time.fixedDeltaTime;

        Vector3 dist = player.transform.position - player.positionBuffer;
        float angle = Mathf.Atan2(dist.y, dist.x) / 3.14f * 180;
        player.visual.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

        //îRóøÇå∏ÇÁÇ∑
        player.fuel.Use();

        //écëú
        if (afterimageTimer > 0.03f) {
            GameObject obj = Instantiate(player.afterimage, player.visual.transform.position, player.visual.transform.rotation);
            obj.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);
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
