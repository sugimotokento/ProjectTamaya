using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAction : PlayerAction {
    protected float accelerationBaseSpeed = 10;

    private const float CHANGE_ACTION_INTERVAL = 1.4f;
    private float rightCrickTimer = 0;
    private bool isLeftMouseDown = false;

    public override void Init(Player p) {
        base.Init(p);
        rightCrickTimer = 0;
    }

    public override void UpdateAction() {
        if (Input.GetMouseButtonDown(0)) {
            isLeftMouseDown = true;
        }
    }

    public override void Action() {
        Move();
        ChangeAction();
    }


    protected virtual void Move() {
        if (isLeftMouseDown==true && player.fuel.canUse == true) {
            player.sound.PlayShot(PlayerSound.SoundIndex.MOVE);

            //�}�E�X�J�[�\����3D���W�ƃv���C���[�̍��W�̋���������ĉ�������������v�Z
            Vector3 dist = GetWorldMousePos() - player.transform.position;
            dist.z = 0;
            player.moveSpeed += dist.normalized * accelerationBaseSpeed;

            player.fuel.Use(); //�R�������炷
        }

        player.transform.position += player.moveSpeed * Time.fixedDeltaTime;
        isLeftMouseDown = false;
    }


    private void ChangeAction() {
        if (Input.GetMouseButton(1)) {
            rightCrickTimer += Time.fixedDeltaTime;

            if (rightCrickTimer >= CHANGE_ACTION_INTERVAL) {
                player.ChangeAction<PlayerMoveAction, PlayerIaiAction>();
            }
        } else {
            //�^�b�N�����[�h
            if (rightCrickTimer > 0 && rightCrickTimer < CHANGE_ACTION_INTERVAL) {

                //�N���X�̐؂�ւ�
                player.ChangeAction<PlayerMoveAction, PlayerTackleAction>();
               

            }
        }
    }

    
}
