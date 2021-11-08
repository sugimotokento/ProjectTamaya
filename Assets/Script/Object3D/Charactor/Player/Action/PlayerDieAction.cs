using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDieAction : PlayerAction {
    public override void Action() {
        player.transform.Rotate(new Vector3(0, 0, 10));
    }
}
