using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerFuel : MonoBehaviour {
    [SerializeField]
    private Image boostGauge;

    private float maxFuel = 100;
    private float fuel;

    private bool canUse = true;


    //UIが変な形してるから調整用
    private const float START_LATE = 0.77f;
    private const float END_LATE = 0.02f;

    // Start is called before the first frame update
    void Start() {
        fuel = maxFuel * START_LATE;

    }

    // Update is called once per frame
    void Update() {
        boostGauge.fillAmount = fuel / maxFuel;

        //燃料を一度使い切ったら再チャージする
        if (fuel > maxFuel * START_LATE) {
            fuel = maxFuel * START_LATE;
            canUse = true;

        } else {
            Charge();

        }

        if (canUse == true) {
            boostGauge.color = Color.white;
        } else {
            boostGauge.color = Color.red;
        }
    }


    private void Charge() {
        if (canUse == true) {
            fuel += Time.deltaTime * 14 * (START_LATE - END_LATE);
        } else {
            fuel += Time.deltaTime * 40 * (START_LATE - END_LATE);
        }
    }

    public void Use() {
        fuel -= 35 * Time.fixedDeltaTime * (START_LATE - END_LATE);

        if (fuel <= maxFuel * END_LATE) {
            fuel = maxFuel * END_LATE;
            canUse = false;
        }
    }


    public bool GetCanUse() {
        return canUse;
    }

}
