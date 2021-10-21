using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
    private Player playerScript;
    public float len;

    // Start is called before the first frame update
    void Start() {
        playerScript = StageManager.instance.player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        if (playerScript.CheckAction<PlayerGuruguruAction>() == true) {
            GuruguruCameraMove();
        } else {
            NormalCameraMove();
        }
    }

    void NormalCameraMove() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        Vector3 cameraPos = this.transform.position + Vector3.back * -10;
        Vector3 followPos = mousePos - playerScript.transform.position + playerScript.transform.position;


        if ((followPos - playerScript.transform.position).magnitude > len) {
            followPos = followPos.normalized * len + playerScript.transform.position;
        }

        Vector3 dist = followPos - cameraPos;


        this.transform.position += dist * 0.5f * Time.deltaTime * 5;
    }
    void GuruguruCameraMove() {
        Vector3 cameraPos = this.transform.position + Vector3.back * -10;
        Vector3 followPos = playerScript.transform.position;

        Vector3 dist = followPos - cameraPos;


        this.transform.position += dist * 0.5f * Time.deltaTime * 5;
    }
}
