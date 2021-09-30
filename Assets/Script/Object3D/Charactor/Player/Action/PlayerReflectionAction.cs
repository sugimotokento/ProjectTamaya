using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReflectionAction : PlayerAction {
    public override void Init(Player p) {
        base.Init(p);
    }

    public override void Action() {
        RaycastHit hit;
        bool isHit = Physics.BoxCast(transform.position, Vector3.one, transform.forward, out hit, transform.rotation);

        if (isHit == true) {

        }
    }
}
