using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageViewEvent : GameEvent {
    [SerializeField] GameObject visual;
    public Vector3[] start = new Vector3[2];
    public Vector3[] end = new Vector3[2];
    public float[] late = new float[2];
    public float[] moveSpeedLate = new float[2];

    LerpCameraAction cameraAction;
    private Vector3 startCameraPos;
    private int index;
    private bool isSetCameraAction = true;
    // Start is called before the first frame update
    void Start() {
        isEvent = true;
    }

    // Update is called once per frame
    void Update() {
        if (canEvent == true) {
            if (isEvent == true) {
                visual.SetActive(true);
                if (isSetCameraAction == true) {
                    isSetCameraAction = false;

                    //カメラの設定
                    StageManager.instance.camera.SetAction<LerpCameraAction>();
                    cameraAction = StageManager.instance.camera.GetAction<LerpCameraAction>();

                }

                //線形補完で移動させる
                cameraAction.start = start[index];
                cameraAction.end = end[index];
                cameraAction.SetLate(late[index]);
                late[index] += 0.2f * Time.deltaTime*moveSpeedLate[index];
                if (late[index] > 1) {
                    index++;
                }


                SkipEvent();
                //終了
                if (index >= start.Length || isEvent == false) {
                    isEvent = false;
                    canEvent = false;
                    visual.SetActive(false);
                    StageManager.instance.camera.SetAction<PlayerCameraAction>();
                }
            }
        }

    }
}
