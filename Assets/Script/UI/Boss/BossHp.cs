using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour {
    [SerializeField] private Image hpGauge;
    [SerializeField] private Boss boss;
    [HideInInspector] public bool isBattle = false;
    // Start is called before the first frame update
    void Start() {
        hpGauge.fillAmount = 0;
    }

    // Update is called once per frame
    void Update() {
        if (isBattle == true) {
            hpGauge.fillAmount = (float)boss.hp / (float)5;
           
        }

    }
    public void SetFillAmount(float fill) {
        hpGauge.fillAmount = fill;
    }
}
