using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {

    private float moveSpeed = 10;

    private void FixedUpdate() {
        this.transform.position += transform.up * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Stage") || other.gameObject.CompareTag("Player")) {
            Destroy(this.gameObject);
        }
    }

    public void SetMoveSpeed(float speed) {
        moveSpeed = speed;
    }
}
