using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public Player player;

    [SerializeField] private Image gauge;

    private float damageAnimationTimer=0;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        damageAnimationTimer += Time.deltaTime;
        if (player.hP.GetIsDamageAnimation() == true) {
            this.transform.localPosition = Vector3.right * Mathf.Sin(damageAnimationTimer * 200) * 20;
            gauge.color = Color.red;

        } else {
            this.transform.localPosition = Vector3.zero;

            gauge.color = Color.white;
        }
    }
}
