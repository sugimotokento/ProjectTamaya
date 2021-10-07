using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIaiAction : PlayerAction {

    float moveLate = 0;
    public bool isIai = false;
    private bool isIaiAttack = false;
    private bool isMouseDown = false;

    private Vector3 chargePosition;
    private Vector3 attackPosition;
    public Vector3 moveSpeed;

    public override void Init(Player p) {
        base.Init(p);
    }

    public override void UpdateAction() {
        if (Input.GetMouseButtonDown(1)) {
            isMouseDown = true;
        }
    }

    public override void Action() {
        

        
    }

}
