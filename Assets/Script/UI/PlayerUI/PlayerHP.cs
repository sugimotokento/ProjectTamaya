using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour {
    [SerializeField] private Player player;
    [SerializeField] private Image hpGauge;
    [SerializeField] private Text number;

    [SerializeField] private int maxHp;
    [SerializeField] private float damageInterval;

    private int hp;
    private float damageIntervalTimer = 0;
    private float damageAnimetionTimer = 0;
    private float alertIntervalTimer = 0;


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
        Alert();
        damageIntervalTimer -= Time.deltaTime;
        damageAnimetionTimer -= Time.deltaTime;
    }

    private void Alert() {
        if (hp <= 30) {
            alertIntervalTimer += Time.deltaTime;
            if (alertIntervalTimer > 1.0f) {
                alertIntervalTimer = 0;
                player.sound.PlayShot(PlayerSound.SoundIndex.PINCHI);
            }
        }
    }

    public int GetMaxHp() { return maxHp; }
    public int GetHp() { return hp; }
    public void Damage(int damage, bool isHeatDamage = false) {
        if (damageIntervalTimer <= 0) {
            hp -= damage;
            ScoreManager.damageNum++;
            damageIntervalTimer = damageInterval;//–³“GŽžŠÔ‚ÌÝ’è
            damageAnimetionTimer = 0.6f;

            if (isHeatDamage == true) {
                player.sound.PlayShot(PlayerSound.SoundIndex.HIT_PIPE);
            } else {
                player.sound.PlayShot(PlayerSound.SoundIndex.DAMAGE);
            }

            if (hp > maxHp) {
                hp = maxHp;

            } else if (hp < 0) {
                hp = 0;
            }
        }
    }
    public void Heal(int heal=40) {
        hp += heal;
        if (hp > maxHp) {
            hp = maxHp;
        }
    }
    public bool GetIsDamageAnimation() {
        return damageAnimetionTimer > 0;
    }
    public bool GetIsDamageInterval() {
        return damageIntervalTimer > 0;
    }
}