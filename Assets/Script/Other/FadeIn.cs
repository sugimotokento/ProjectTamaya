using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
    [SerializeField] private Image image;
    private float alpha = 1;
    bool isFadeEnd = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        image.color = new Color(0, 0, 0, alpha);
        if (isFadeEnd == false) {
            alpha -= Time.deltaTime;
        }


        if (alpha < 0) {
            isFadeEnd = true;
            image.enabled = false;
        }
    }

    public bool GetIsFadeEnd() { return isFadeEnd; }
}
