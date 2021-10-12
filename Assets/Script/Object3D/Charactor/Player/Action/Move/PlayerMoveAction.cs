using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAction : PlayerAction {
    protected float accelerationBaseSpeed = 10;

    private float rightCrickTimer = 0;
    private const float CHANGE_ACTION_INTERVAL = 1.4f;

    public override void Init(Player p) {
        base.Init(p);
        rightCrickTimer = 0;
    }

    public override void UpdateAction() {
        if (Input.GetMouseButtonDown(0) && player.fuel.canUse == true) {
            player.audioSource.PlayOneShot(player.sound[0]);
        }
    }

    public override void Action() {
        Debug.Log("�ړ�");

        Move();


        ChangeAction();
    }


    protected virtual void Move() {
        if (Input.GetMouseButton(0) && player.fuel.canUse == true) {
            //�}�E�X�J�[�\����3D���W�ƃv���C���[�̍��W�̋���������ĉ�������������v�Z
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 dist = mousePos - player.transform.position;
            dist.z = 0;
            player.moveSpeed += dist.normalized * accelerationBaseSpeed * Time.fixedDeltaTime;

            player.fuel.UseFuel();
        }

        player.transform.position += player.moveSpeed * Time.fixedDeltaTime;
    }


    private void ChangeAction() {
        if (Input.GetMouseButton(1)) {
            rightCrickTimer += Time.fixedDeltaTime;

        } else {
            //�^�b�N�����[�h
            if (rightCrickTimer > 0 && rightCrickTimer < CHANGE_ACTION_INTERVAL) {
                Debug.Log("�^�b�N���`�F���W");

                //�}�E�X���W���擾
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10.0f;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                //�N���X�̐؂�ւ�
                player.ChangeAction<PlayerMoveAction, PlayerTackleAction>();
                PlayerTackleAction tackleAction = player.GetAction<PlayerTackleAction>();
                tackleAction.mousePos = mousePos;
            }

            //�������[�h
            if (rightCrickTimer >= CHANGE_ACTION_INTERVAL) {
                Debug.Log("�����`�F���W");
                //player.ChangeAction<PlayerMoveAction, PlayerIaiAction>();
                //PlayerIaiAction iaiAction = player.GetAction<PlayerIaiAction>();
            }
        }
    }
}
