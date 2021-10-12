using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerFuel : MonoBehaviour {
    float maxFuel = 100;
    float fuel;

    public bool canUse = true;

    private Image gauge;
    // Start is called before the first frame update
    void Start() {
        fuel = maxFuel;

        gauge = transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        gauge.fillAmount = fuel / maxFuel;

        if (fuel > maxFuel) {
            fuel = maxFuel;
            canUse = true;
        } else {

            ChargeFuel();
            
        }
    }


    private void ChargeFuel() {
        if (canUse == true) {
            fuel += Time.deltaTime * 10;
            gauge.color = Color.white;
        } else {
            fuel += Time.deltaTime * 20;
            gauge.color = Color.red;
        }
    }

    public void UseFuel() {
        fuel -= Time.deltaTime * 20;

        if (fuel < 0) {
            fuel = 0;
            canUse = false;
        }
    }


}