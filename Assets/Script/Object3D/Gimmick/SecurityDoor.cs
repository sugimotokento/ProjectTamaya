using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityDoor : MonoBehaviour {
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject interact;
    [SerializeField] private float moveSpeedLate;
    [SerializeField] private int needKey = 1;

    private bool isOpen = false;
    private bool isInputMouseDown = false;
    private float late;

    private void Start() {
        interact.SetActive(false);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            isInputMouseDown = true;
        }
    }

    private void FixedUpdate() {
        if (isOpen == true) {
            late += Time.fixedDeltaTime * moveSpeedLate;
            if (late > 1) late = 1;

            door.transform.position = Vector3.Lerp(transform.position, point.transform.position, late);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            isInputMouseDown = false;
        }
    }


    private void OnTriggerStay(Collider other) {
        if (isOpen == false) {
            if (other.gameObject.CompareTag("Player")) {
                //ÉJÉMÇéùÇ¡ÇƒÇ¢ÇΩÇÁäJÇ≠
                Player player = other.gameObject.GetComponent<Player>();
                if (player.item.GetKey() >= needKey && isInputMouseDown == true) {
                    player.item.UseKey();
                    interact.SetActive(false);
                    isOpen = true;
                } else {
                    interact.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (isOpen == false) {
            if (other.gameObject.CompareTag("Player")) {
                interact.SetActive(false);

            }
        }
    }
}
