using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {

    [SerializeField]
    private int maxHp;
    private int hp;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    public int GetMaxHp() { return maxHp; }
    public int GetHp() { return hp; }
    public void AddHp(int add) {
        hp += add;


        if (hp > maxHp) {
            hp = maxHp;

        }else if (hp < 0) {
            hp = 0;
        }
    }
}
