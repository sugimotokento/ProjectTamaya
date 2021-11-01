using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationRing : MonoBehaviour {
    [SerializeField] private float accelerationLate;

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Player player = other.gameObject.GetComponent<Player>();

            Vector3 dist = player.transform.position - this.transform.position;
            player.moveSpeed += this.transform.right * dist.magnitude * accelerationLate;
        }
    }
}
