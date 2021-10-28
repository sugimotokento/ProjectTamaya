using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationRing : MonoBehaviour {
    [SerializeField] private float accelationSpeed;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Player player = other.gameObject.GetComponent<Player>();

            player.moveSpeed = this.transform.right * accelationSpeed;
        }
    }
}
