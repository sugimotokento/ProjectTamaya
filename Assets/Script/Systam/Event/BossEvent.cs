using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : GameEvent {
    private bool isSetCamera = false;
    // Start is called before the first frame update
    void Start() {
        isEvent = true;
    }

    // Update is called once per frame
    void Update() {

        if (canEvent == true) {
            if (isSetCamera == false) {
                isSetCamera = true;
                isEvent = false;
                StageManager.instance.camera.SetAction<PointCameraAction>();
                StageManager.instance.camera.GetAction<PointCameraAction>().SetPoint(new Vector3(0, 0, -20));
            }
        }
    }
}
