using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCameraAction : CameraAction {
    Vector3 point;
    public override void Action() {
        Vector3 dist = point - camera.transform.position;

        camera.transform.position += dist * 0.5f * Time.deltaTime * 5;
    }

    public void SetPoint(Vector3 pos) {
        point = pos;
    }
}
