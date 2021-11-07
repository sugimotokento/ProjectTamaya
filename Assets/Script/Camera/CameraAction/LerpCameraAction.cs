using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCameraAction : CameraAction {
    public Vector3 start;
    public Vector3 end;
    private float late;

    public override void Action() {
        camera.transform.position = Vector3.Lerp(start, end, late);
    }

    public float GetLate() {
        return late;
    }
    public void SetLate(float num) {
        late = num;
    }
    public void AddLate(float add) {
        late += add;
        if (late > 1) {
            late = 1;
        }
    }
}
