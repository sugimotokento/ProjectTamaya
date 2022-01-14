using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossClearEvent : GameEvent {
    [SerializeField] private FadeOut2 fade;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (StageManager.instance.boss != null ) {
            Boss boss = StageManager.instance.boss;
            if (boss.CheckAction<BossDieAction>() == true && boss.GetAction<BossDieAction>().isDieAnimationEnd == true) {
                isEvent = true;
            }
        }

        if (canEvent == true) {
            if (isEvent == true) {
                fade.gameObject.SetActive(true);

                if (fade.GetIsFadeEnd() == true) {
                    SceneManager.LoadScene("Title");
                }
            }
        }
    }
}
