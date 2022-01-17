using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {
    [HideInInspector] public bool isObjectMode = false;
    [HideInInspector] public bool isHit = false;
    [HideInInspector] public bool isDestroy = false;
    [SerializeField] private GameObject effect;

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

    private void OnDestroy() {
        Destroy(Instantiate(effect, this.transform.position, Quaternion.identity), 0.5f);
    }

    private void OnTriggerEnter(Collider other) {
       
        if (other.gameObject.CompareTag("Stage") || other.gameObject.CompareTag("StageEX")) {
            isHit = true;
            if (isObjectMode == false) {
                Destroy(this.gameObject);
            }
        }

        if (other.gameObject.CompareTag("Player")) {
            isHit = true;
            other.gameObject.GetComponent<Player>().hP.Damage(35);
            if (isObjectMode == false) {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetMoveSpeed(float speed) {
        moveSpeed = speed;
    }
}
