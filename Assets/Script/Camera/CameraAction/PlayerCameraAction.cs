using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraAction : CameraAction {
    float range = 2;
    float iaiTimer = 0;

    public override void Init(GameCamera cam) {
        base.Init(cam);
    }

    public override void Action() {
        //‹‡’†‚ÉƒJƒƒ‰‚ğˆø‚­
        if(StageManager.instance.player.CheckAction<PlayerIaiAction>() == true) {
            if (StageManager.instance.player.GetAction<PlayerIaiAction>().isCharge == true) {
                iaiTimer += 20 * Time.fixedDeltaTime;
            } else {
                iaiTimer = 0;
            }
        } else {
            iaiTimer = 0;
        }
        if (iaiTimer > 10) {
            iaiTimer = 10;
        }


        Vector3 mousePos = Input.mousePosition;
        mousePos.z = camera.transform.position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        float late = 0.5f;

        Vector3 cameraPos = camera.transform.position + Vector3.back * (-12 - iaiTimer);
        Vector3 followPos = -StageManager.instance.player.transform.position + (mousePos * 0.5f * (1 - late) + StageManager.instance.player.moveSpeed * late);

        //ƒJƒƒ‰‚Ì‹——£‚Ì§ŒÀ
        if ((followPos - StageManager.instance.player.moveSpeed * late).magnitude > range) {
            followPos = followPos.normalized * range + StageManager.instance.player.moveSpeed * late + StageManager.instance.player.transform.position;
        }

        Vector3 dist = followPos - cameraPos;

        camera.transform.position += dist * 0.5f * Time.deltaTime * 5;
    }
}
