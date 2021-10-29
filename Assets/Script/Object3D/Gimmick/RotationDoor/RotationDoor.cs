using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDoor : MonoBehaviour {
    private Player player;

    [HideInInspector] public bool isRotation = false;
    [SerializeField] private GameObject standPointUp;
    [SerializeField] private GameObject standPointDown;
    private GameObject standObj;

    private float startAngle;
    private float rotationLate = 0;

    private void FixedUpdate() {
        if (isRotation == true) {
            float angle = Mathf.Lerp(startAngle, startAngle + 180, rotationLate);
            this.transform.eulerAngles = Vector3.forward*angle;
            rotationLate += Time.fixedDeltaTime;
            player.transform.position= standObj.transform.position;

            if (rotationLate >= 1) {
                isRotation = false;
                this.transform.eulerAngles = Vector3.forward * (startAngle + 180);
                player.AddAction<PlayerMoveAction>();
            }
        }
    }


    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            player = collision.gameObject.GetComponent<Player>();

            //プレイヤーを移動出来なくする
            player.RemoveAction<PlayerMoveAction>();
            player.moveSpeed = Vector3.zero;

            Vector3 distUp = collision.gameObject.transform.position - standPointUp.transform.position;
            Vector3 distDown = collision.gameObject.transform.position - standPointDown.transform.position;
            //近い方の待機地点に移動する
            if (distUp.magnitude > distDown.magnitude) {
                collision.gameObject.transform.position = standPointDown.transform.position;
                standObj = standPointDown;
            } else {
                collision.gameObject.transform.position = standPointUp.transform.position;
                standObj = standPointUp;
            }
            startAngle = this.transform.eulerAngles.z;
            rotationLate = 0;
            isRotation = true;
        }
    }
}
