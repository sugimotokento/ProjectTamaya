using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusuma : MonoBehaviour {
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject door;
    [SerializeField] private float moveSpeedLate;

    private bool isOpen = false;
    private float late;

    private void FixedUpdate() {
        if (isOpen == true) {
            late += Time.fixedDeltaTime * moveSpeedLate;
            if (late > 1) late = 1;
           
        } else {
            late -= Time.fixedDeltaTime * moveSpeedLate;
            if (late < 0) late = 0;
        }

        door.transform.position = Vector3.Lerp(transform.position, point.transform.position, late);
        isOpen = false;
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            isOpen = true;
        }

    }
}
