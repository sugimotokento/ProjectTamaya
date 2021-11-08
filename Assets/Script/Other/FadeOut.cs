using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {
    [SerializeField] private Image image;
    private float alpha = 0;
    bool isFadeEnd = false;
    private float addLate = 1;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        image.color = new Color(0, 0, 0, alpha);
        if (isFadeEnd == false) {
            alpha += Time.deltaTime*addLate;
        }

        if (alpha > 1) {
            isFadeEnd = true;
        }
    }
    public void SetActive(bool flag) {
        image.enabled = flag;
    }
    public void SetAddLate(float late) {
        addLate = late;
    }
    public bool GetIsFadeEnd() { return isFadeEnd; }

}
