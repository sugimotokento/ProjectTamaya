using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

    [SerializeField] private GameObject visual;
    [SerializeField] private float reflectionPower;
    private bool isAnimation = false;
    private float animationTimer = 0;

    private const float ANIMATION_TIME = 0.5f;

    private void Update() {
        if (isAnimation == true) {
            animationTimer += Time.deltaTime;

            visual.transform.localScale += Vector3.one * Mathf.Sin(animationTimer*50)*0.03f;
            if (animationTimer > ANIMATION_TIME) {
                isAnimation = false;
                animationTimer = 0;
                visual.transform.localScale = Vector3.one;//‘å‚«‚³‚ð–ß‚·
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag=="Player") {
            Player player = collision.gameObject.GetComponent<Player>();
            Vector3 dist = collision.gameObject.transform.position - this.transform.position;
            dist.z = 0;
            float moveSpeed = player.moveSpeed.magnitude;

            //”½ŽË
            player.moveSpeed = dist.normalized * moveSpeed * reflectionPower;
            isAnimation = true;
        }
    }
}
