using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearEvent : GameEvent {
    [SerializeField] private GameObject ui;



    // Update is called once per frame
    void Update() {
        if (StageManager.instance.isClear == true) {
            isEvent = true;
        }
        if (canEvent == true) {

            Fade();
        }
    }


    private void Fade() {

        StageManager.instance.camera.SetAction<PlayerCameraAction>();
        StageManager.instance.player.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        ui.SetActive(true);



    }

}
