using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityDoor : MonoBehaviour {
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject door;
    [SerializeField] private float moveSpeedLate;
    [SerializeField] private int needKey=1;

    private bool isOpen = false;
    private float late;


    private void FixedUpdate() {
        if (isOpen == true) {
            late += Time.fixedDeltaTime * moveSpeedLate;
            if (late > 1) late = 1;

            door.transform.position = Vector3.Lerp(transform.position, point.transform.position, late);
        }
    }



    private void OnTriggerStay(Collider other) {
        if (isOpen == false) {
            if (other.gameObject.CompareTag("Player")) {
                //ƒJƒM‚ğ‚Á‚Ä‚¢‚½‚çŠJ‚­
                Player player = other.gameObject.GetComponent<Player>();
                if (player.item.GetKey() >= needKey) {
                    player.item.UseKey();
                    isOpen = true;
                }
            }
        }
    }
}
