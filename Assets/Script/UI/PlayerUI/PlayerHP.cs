using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour {
    [SerializeField] private Image hpGauge;
    [SerializeField] private Text number;

    [SerializeField] private int maxHp;
    [SerializeField] private float damageInterval;

    private int hp;
    private float damageIntervalTimer = 0;
    private float damageAnimetionTimer = 0;

    private bool isDie = false;



    // Start is called before the first frame update
    void Start() {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update() {
        hpGauge.fillAmount = (float)hp / (float)maxHp;
        number.text = hp.ToString() + "/" + maxHp.ToString();
        if (hp <= 30) {
            number.color = Color.red;
        } else {
            number.color = Color.white;
        }

        damageIntervalTimer -= Time.deltaTime;
        damageAnimetionTimer -= Time.deltaTime;
    }



    public int GetMaxHp() { return maxHp; }
    public int GetHp() { return hp; }
    public void Damage(int damage) {
        if (damageIntervalTimer <= 0) {
            hp -= damage;
            damageIntervalTimer = damageInterval;//–³“GŽžŠÔ‚ÌÝ’è
            damageAnimetionTimer = 0.6f;

            if (hp > maxHp) {
                hp = maxHp;

            } else if (hp < 0) {
                hp = 0;
            }
        }
    }

    public bool GetIsDamageAnimation() {
        return damageAnimetionTimer > 0;
    }
    public bool GetIsDamageInterval() {
        return damageIntervalTimer > 0;
    }
}