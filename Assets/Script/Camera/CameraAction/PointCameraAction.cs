using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCameraAction : CameraAction {
    Vector3 point;
    float addLate = 1;
    public override void Action() {
        Vector3 dist = point - camera.transform.position;

        camera.transform.position += dist * 0.5f * Time.deltaTime * 5* addLate;
    }

    public void SetPoint(Vector3 pos) {
        point = pos;
    }
    public void SetAddLate(float late) {
        addLate = late;
    }
}
