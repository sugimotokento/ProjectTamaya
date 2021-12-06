using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearEvent : GameEvent {
    [SerializeField] private GameObject ui;
    [SerializeField] private Image[] images;
    [SerializeField] private Text[] texts;

    [SerializeField] private Text airText;

    private float alpha = 0;
    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < images.Length; ++i) {
            images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, alpha);
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
           
            Fade();
            AirScore();
        }
    }


    private void Fade() {

        StageManager.instance.camera.SetAction<PlayerCameraAction>();
        StageManager.instance.player.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        ui.SetActive(true);

        alpha += Time.deltaTime * 4;
        for (int i = 0; i < images.Length; ++i) {
            images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, alpha);
        }
        for (int i = 0; i < texts.Length; ++i) {
            texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, alpha);
        }

    }
    private void AirScore() {
        float air = ScoreManager.GetAir();

        //分、秒、少数ごとに分ける
        int minutes = (int)(air / 60);
        int seconds = (int)(air % 60);
        int decimals = (int)((air - Mathf.FloorToInt(air)) * 100);

        //各桁を00で表示
        string munutesText = minutes.ToString();
        string secondsText = seconds.ToString();
        string decimalsText = decimals.ToString();

        if (minutes < 10) {
            munutesText = "0" + minutes;
        }
        if (seconds < 10) {
            secondsText = "0" + seconds;
        }
        if (decimals < 10) {
            decimalsText = "0" + decimals;
        }

        //テキストに残り時間を設定する
        airText.text = munutesText + ":" + secondsText + ":" + decimalsText;
    }
}
