using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIaiAction : PlayerMoveAction {

    float moveLate = 0;
    public bool isIai = false;
    private bool isIaiAttack = false;
    private bool isMouseDown = false;

    private Vector3 attackPosition;

    public override void Init(Player p) {
        base.Init(p);
    }

    public override void UpdateAction() {
        if (Input.GetMouseButtonDown(1)) {
            isMouseDown = true;
        }
    }

    public override void Action() {
        if (isIai == false) {
            if (isMouseDown == true) {
                isMouseDown = false;
                isIai = true;
            }

            if (isIai == true) {

                if (isMouseDown == true) {
                    isMouseDown = false;
                    isIaiAttack = true;

                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = 10; //�J�����t�@�[�̐ݒ�
                    mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                    mousePos.z = 0; //�ϊ���̍��W��0�ɂ���

                    attackPosition = mousePos;
                }
            }


            if (isIaiAttack == true) {

                moveLate += Time.fixedDeltaTime;
                player.transform.position = Vector3.Lerp(player.transform.position, attackPosition, moveLate);

                //�����̏I��
                if (moveLate > 1) {
                    isIai = false;
                    isIaiAttack = false;
                    moveLate = 0;


                }
            }
        }

    }
}