using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour {
    protected GameCamera camera;

    public virtual void Init(GameCamera cam) {
        camera = cam;
    }
    public virtual void Action() { }

}
