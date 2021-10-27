using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAir : MonoBehaviour {
    [SerializeField]
    private Image gauge;
    [SerializeField]
    private Text number;

    [SerializeField]
    private float startMaxTimer;
    private const float MAX_TIME = 12 * 60;
    private float timer;


    // Start is called before the first frame update
    void Start() {
        timer = startMaxTimer * 60;
    }

    // Update is called once per frame
    void Update() {
        gauge.fillAmount = timer / MAX_TIME;

        SetText();
    }

    private void SetText() {
        //���A�b�A�������Ƃɕ�����
        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);
        int decimals = (int)((timer - Mathf.FloorToInt(timer)) * 100);

        //�e����00�ŕ\��
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

        //�e�L�X�g�Ɏc�莞�Ԃ�ݒ肷��
        number.text = munutesText + ":" + secondsText + ":" + decimalsText;
        timer -= Time.deltaTime;
    }
}
