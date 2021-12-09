using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDoor : MonoBehaviour {
    private Player player;

    [HideInInspector] public bool isRotation = false;

    [SerializeField] private GameObject standPointUp;
    [SerializeField] private GameObject standPointDown;
    [SerializeField] private GameObject interact;

    private GameObject standObj;

    private const float INTERACT_INTERVAL = 3;

    private float startAngle;
    private float rotationLate = 0;
    private float interactIntervalTimer = 0;

    private bool isInputMouseDown = false;
    private bool isInteractInterval = false;

    private void Update() {
        if (Input.GetMouseButtonDown(0) == true) {
            isInputMouseDown = true;
        }
    }

    private void FixedUpdate() {
        Rotation();

        if (isInteractInterval == true) {
            interactIntervalTimer += Time.fixedDeltaTime;
            if (interactIntervalTimer > INTERACT_INTERVAL) {
                isInteractInterval = false;
            }
        }

        interact.transform.rotation = Quaternion.identity;
    }

    void Rotation() {
        if (isRotation == true) {
            float angle = Mathf.Lerp(startAngle, startAngle + 180, rotationLate);
            this.transform.eulerAngles = Vector3.forward * angle;
            rotationLate += Time.fixedDeltaTime;

            //プレイヤーの座標を待機地点に固定する
            player.transform.position = standObj.transform.position;
            player.visual.transform.eulerAngles = Vector3.forward * (angle + 180);

            //回転終了
            if (rotationLate >= 1) {
                isRotation = false;
                this.transform.eulerAngles = Vector3.forward * (startAngle + 180);
                player.AddAction<PlayerMoveAction>();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            isInputMouseDown = false;
        }
    }
    private void OnTriggerStay(Collider other) {
        if (isInteractInterval == true) isInputMouseDown = false;

        if (other.gameObject.CompareTag("Player")) {
            if (isInputMouseDown == true && isRotation == false) {
                player = other.gameObject.GetComponent<Player>();

                //プレイヤーを移動出来なくする
                player.RemoveAction<PlayerMoveAction>();
                player.moveSpeed = Vector3.zero;

                Vector3 distUp = other.gameObject.transform.position - standPointUp.transform.position;
                Vector3 distDown = other.gameObject.transform.position - standPointDown.transform.position;
                //近い方の待機地点に移動する
                if (distUp.magnitude > distDown.magnitude) {
                    other.gameObject.transform.position = standPointDown.transform.position;
                    standObj = standPointDown;
                } else {
                    other.gameObject.transform.position = standPointUp.transform.position;
                    standObj = standPointUp;
                }
                startAngle = this.transform.eulerAngles.z;
                rotationLate = 0;
                isRotation = true;
                isInteractInterval = true;
                interactIntervalTimer = 0;
            }

            if (isRotation == false && isInteractInterval==false) {
                interact.SetActive(true);
            } else {
                interact.SetActive(false);
                
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            interact.SetActive(false);
        }
    }
}
