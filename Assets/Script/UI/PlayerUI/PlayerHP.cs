using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour {

    [SerializeField]
    private Image hpGauge;
    [SerializeField]
    private Text number;

    [SerializeField]
    private int maxHp;
    private int hp;

    private bool isDie = false;

    // Start is called before the first frame update
    void Start() {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update() {
        hpGauge.fillAmount = (float)hp / (float)maxHp;
        number.text = hp.ToString() + "/" + maxHp.ToString();
    }


    public int GetMaxHp() { return maxHp; }
    public int GetHp() { return hp; }
    public void Damage(int damage) {
        hp -= damage;

        if (hp > maxHp) {
            hp = maxHp;

        }else if (hp < 0) {
            hp = 0;
        }
    }
}
