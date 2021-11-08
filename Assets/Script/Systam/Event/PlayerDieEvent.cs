using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDieEvent : GameEvent {
    [SerializeField] FadeOut fade;

    private bool canSetParam = true;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (StageManager.instance.player.CheckAction<PlayerDieAction>() == true) {
            isEvent = true;
        }
        if (canEvent == true) {

            if (canSetParam == true) {
                StageManager.instance.camera.SetAction<PointCameraAction>();
                StageManager.instance.camera.GetAction<PointCameraAction>().SetPoint(StageManager.instance.player.gameObject.transform.position - Vector3.forward * 6);
                StageManager.instance.camera.GetAction<PointCameraAction>().SetAddLate(0.35f);
            }
            fade.SetAddLate(0.2f);
            fade.gameObject.SetActive(true);

            if (fade.GetIsFadeEnd() == true) {
                SceneManager.LoadScene("GameOver");
            }

        }
    }
}
