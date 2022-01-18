using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoalAction : PlayerAction
{
    private bool isGoal = false;
    public override void Action() {

        if (isGoal == true) {
            player.visual.transform.rotation = Quaternion.Euler(0, -90, 0);
            player.moveSpeed = Vector3.zero;
            player.animator.SetBool("isMove", false);
            player.animator.SetBool("isCharge", false);
            player.animator.SetBool("isTacle", false);
            player.animator.SetBool("isDown", false);
            player.animator.SetBool("isSpin", false);
            player.animator.SetBool("isIai", false);
            player.animator.SetBool("isGoal", true);

            
        }


    }

    public override void TriggerEnter(Collider collider) {
        if(collider.gameObject.name=="Goal") {
            player.ReMoveActionAll();
            player.AddAction<PlayerGoalAction>();
            player.GetAction<PlayerGoalAction>().SetIsGoal(true);


            for(int i=0; i<player.line.Length; ++i) {
                player.line[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetIsGoal(bool flag) {
        isGoal = flag;
    }
}
