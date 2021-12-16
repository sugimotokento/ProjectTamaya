using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusuma : MonoBehaviour {
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject door;
    [SerializeField] private AudioSource sound;
    [SerializeField] private float moveSpeedLate;
    [SerializeField] private float closeInterval;

    private float closeIntervalTimer = 0;
    private float late;

    private bool isOpen = false;
    private bool canPlaySound = true;

    private void FixedUpdate() {
        if (isOpen == true) {
             late += Time.fixedDeltaTime * moveSpeedLate;
            if (late > 1) late = 1;


            if (canPlaySound == true) {
                canPlaySound = false;
                sound.Play();
            }

        } else {
            closeIntervalTimer += Time.fixedDeltaTime;
           
            if (closeIntervalTimer > closeInterval) {
                late -= Time.fixedDeltaTime * moveSpeedLate;
                if (late < 0) late = 0;


                if (canPlaySound == false) {
                    canPlaySound = true;
                    sound.Play();
                }
            }

         
        }

        door.transform.position = Vector3.Lerp(transform.position, point.transform.position, late);
        isOpen = false;
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            isOpen = true;
            closeIntervalTimer = 0;
        }

    }
}
