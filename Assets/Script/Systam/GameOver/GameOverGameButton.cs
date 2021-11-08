using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverGameButton : MonoBehaviour {
    [SerializeField] FadeIn fadeIn;
    [SerializeField] FadeOut fadeOut;

    bool isChangeScene = false;


    // Update is called once per frame
    void Update() {
        if (isChangeScene == true) {
            fadeOut.gameObject.SetActive(true);
            if (fadeOut.GetIsFadeEnd() == true) {
                SceneManager.LoadScene(StageManager.sceneName);
            }
        }
    }

    public void OnClick() {
        isChangeScene = true;
    }

}
