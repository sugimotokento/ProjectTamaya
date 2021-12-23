using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {
    [SerializeField] private Image clickImage;
    [SerializeField] private FadeOut2 fadeOut;
    private float clickImageAcpha=1;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        clickImageAcpha += Time.deltaTime*1.3f;
        float alpha =1-(Mathf.Sin(clickImageAcpha) + 1) * 0.5f*0.4f;
        clickImage.color = new Color(1, 1, 1, alpha);


        if (Input.GetMouseButtonDown(0)) {
            fadeOut.gameObject.SetActive(true);
            clickImage.gameObject.SetActive(false);
        }

        if (fadeOut.GetIsFadeEnd() == true) {
            SceneManager.LoadScene("Stage1");
        }
    }
}
