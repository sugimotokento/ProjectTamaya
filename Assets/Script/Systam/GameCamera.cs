using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
    private Player playerScript;
    public float distRange;

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

        float late = 0.35f;

        Vector3 cameraPos = this.transform.position + Vector3.back * -15;
        Vector3 followPos = mousePos - playerScript.transform.position + playerScript.moveSpeed* late;


        if ((followPos - playerScript.moveSpeed * late).magnitude > distRange) {
            followPos = followPos.normalized * distRange + playerScript.moveSpeed * late;
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
