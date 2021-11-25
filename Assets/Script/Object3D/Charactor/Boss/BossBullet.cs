using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {
    [HideInInspector] public bool isObjectMode = false;
    [HideInInspector] public bool isHit = false;
    [HideInInspector] public bool isDestroy = false;

    private float destroyTimer =0;
    private float moveSpeed = 10;


    private void FixedUpdate() {
        this.transform.position += transform.up * moveSpeed * Time.fixedDeltaTime;

        destroyTimer += Time.fixedDeltaTime;
        if (destroyTimer > 10) {
            isDestroy = true;
            if (isObjectMode == false) {
                
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
       
        if (other.gameObject.CompareTag("Stage") || other.gameObject.CompareTag("Player")) {
            isHit = true;
            if (isObjectMode == false) {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetMoveSpeed(float speed) {
        moveSpeed = speed;
    }
}
