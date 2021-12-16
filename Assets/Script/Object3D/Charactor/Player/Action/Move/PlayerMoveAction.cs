using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAction : PlayerAction {
    protected float accelerationBaseSpeed;

    GameObject chargeEffect;
    private const float CHANGE_ACTION_INTERVAL = 1.4f;
    private float rightCrickTimer = 0;
    private bool isLeftMouseDown = false;

    public override void Init(Player p) {
        base.Init(p);
        rightCrickTimer = 0;
        accelerationBaseSpeed = 10;

        player.animator.SetBool("isTacle", false);
        player.animator.SetBool("isIai", false);

        chargeEffect = Instantiate(player.iaiEffect, player.visual.transform);
        chargeEffect.SetActive(false);
    }

    public override void UpdateAction() {
        if (Input.GetMouseButtonDown(0) ) {
            isLeftMouseDown = true;
            player.sound.PlayShot(PlayerSound.SoundIndex.BOOST);
        }

        if (Input.GetMouseButtonUp(0)) {
            player.sound.Stop(PlayerSound.SoundIndex.BOOST);
        }
    }

    public override void Action() {
        Move();
        ChangeAction();

    }


    protected virtual void Move() {
        if (Input.GetMouseButton(0) && player.fuel.GetCanUse() == true) {

            //�}�E�X�J�[�\����3D���W�ƃv���C���[�̍��W�̋���������ĉ�������������v�Z
            Vector3 distance = GetWorldMousePos() - player.transform.position;
            distance.z = 0;
            player.moveSpeed += distance.normalized * accelerationBaseSpeed * Time.fixedDeltaTime;
            player.moveSpeed.z = 0;
            player.fuel.Use(); //�R�������炷
        }
        player.transform.position += player.moveSpeed * Time.fixedDeltaTime;

        //�v���C���[�̌����ڂ���]
        Vector3 dist = player.transform.position - player.positionBuffer;
        Vector3 angle = Vector3.zero;
        angle.z = Mathf.Atan2((dist.y), Mathf.Abs(dist.x)) / 3.14f * 180;
        if (dist.x > 0) {
            angle.y = 0;
        } else {
            angle.y = 180;
        }

        player.visual.transform.rotation = Quaternion.Euler(0.0f, angle.y, angle.z);

        isLeftMouseDown = false;

        //���[�V����
        dist = player.gameObject.transform.position - player.positionBuffer;
        if (dist.magnitude < 0.02f) {
            player.animator.SetBool("isMove", false);
        } else {
            player.animator.SetBool("isMove", true);
        }
    }


    private void ChangeAction() {
        if (Input.GetMouseButton(1) && player.fuel.GetCanUse() == true) {
            rightCrickTimer += Time.fixedDeltaTime;

            //�G�t�F�N�g����
            if (rightCrickTimer > 0.5f) {
                chargeEffect.SetActive(true);
                chargeEffect.transform.position = player.gameObject.transform.position;
                chargeEffect.transform.rotation = player.visual.transform.rotation;
            }

            if (rightCrickTimer >= CHANGE_ACTION_INTERVAL) {
                player.ChangeAction<PlayerMoveAction, PlayerIaiAction>();
                player.GetAction<PlayerIaiAction>().SetEffect(chargeEffect);
                player.animator.SetBool("isMove", false);
            }
        } else {
            chargeEffect.SetActive(false);

            //�^�b�N�����[�h
            if (rightCrickTimer > 0 && rightCrickTimer < CHANGE_ACTION_INTERVAL) {

                //�N���X�̐؂�ւ�
                player.ChangeAction<PlayerMoveAction, PlayerTackleAction>();
                
                player.animator.SetBool("isMove", false);

            }
        }
    }

}