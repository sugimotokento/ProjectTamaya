using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClearSceneManager : MonoBehaviour {
    [SerializeField] private FadeOut2 fade;
    bool isChangeScene = false;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0) == true) {
            fade.gameObject.SetActive(true);
        }

        if (fade.GetIsFadeEnd() == true) {
            SceneManager.LoadScene("Title");
        }
    }
}
