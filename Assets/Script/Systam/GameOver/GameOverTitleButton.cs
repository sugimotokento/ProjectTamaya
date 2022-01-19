using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverTitleButton : MonoBehaviour
{
    [SerializeField] FadeIn fadeIn;
    [SerializeField] FadeOut fadeOut;

    bool isChangeScene = false;


    // Update is called once per frame
    void Update() {
        if (isChangeScene == true) {
            fadeOut.gameObject.SetActive(true);
            if (fadeOut.GetIsFadeEnd() == true) {
                SceneManager.LoadScene("Title");
            }
        }
    }

    public void OnClick() {
        isChangeScene = true;
    }
}
