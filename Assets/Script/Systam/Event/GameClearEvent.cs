using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearEvent : GameEvent {
    [SerializeField] private GameObject ui;
    [SerializeField] private Image[] images;
    [SerializeField] private Text[] texts;

    private float alpha = 0;
    // Start is called before the first frame update
    void Start() {
        for(int i=0; i < images.Length; ++i) {
            images[i].color= new Color(images[i].color.r, images[i].color.g, images[i].color.b, alpha);
        }
        for (int i = 0; i < texts.Length; ++i) {
            texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, alpha);
        }
    }

    // Update is called once per frame
    void Update() {
        if (StageManager.instance.isClear == true) {
            isEvent = true;
        }

        if (canEvent == true) {
            StageManager.instance.camera.SetAction<PlayerCameraAction>();
            StageManager.instance.player.visual.transform.rotation = Quaternion.Euler(0, 0, 0);
            ui.SetActive(true);

            alpha += Time.deltaTime*4;
            for (int i = 0; i < images.Length; ++i) {
                images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, alpha);
            }
            for (int i = 0; i < texts.Length; ++i) {
                texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, alpha);
            }
        }
    }
}
