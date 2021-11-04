using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    [SerializeField] private Player player;


    private float damageAnimationTimer=0;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        damageAnimationTimer += Time.deltaTime;
        if (player.hP.GetIsDamageAnimation() == true) {
            this.transform.localPosition = Vector3.right * Mathf.Sin(damageAnimationTimer * 100) * 8;
        } else {
            this.transform.localPosition = Vector3.zero;
        }
    }
}
