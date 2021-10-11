using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTackleAction : PlayerMoveAction
{
    public override void Init(Player p) {
        base.Init(p);
    }

    public override void Action() {
        base.Action();
        Debug.Log("ƒ^ƒbƒNƒ‹");
    }
}
