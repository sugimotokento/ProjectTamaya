using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
    private Player playerScript;
    private Vector3 eventPos;
    public float distRange;
    public bool isEvent=false;

    // Start is called before the first frame update
    void Start() {
        playerScript = StageManager.instance.player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        if (playerScript.CheckAction<PlayerGuruguruAction>() == true) {
            GuruguruCameraMove();

        } else if (isEvent == true) {
            EventCameraMove();

        } else {
            NormalCameraMove();
        }
    }

    void NormalCameraMove() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        float late = 0.1f;

        Vector3 cameraPos = this.transform.position + Vector3.back * -15;
        Vector3 followPos =  - playerScript.transform.position + (mousePos*0.5f*(1-late) + playerScript.moveSpeed * late);

        //�J�����̋����̐���
        if ((followPos - playerScript.moveSpeed * late).magnitude > distRange) {
            followPos = followPos.normalized * distRange + playerScript.moveSpeed * late +playerScript.transform.position;
        }

        Vector3 dist = followPos - cameraPos;

        this.transform.position += dist * 0.5f * Time.deltaTime * 5;
    }
    void GuruguruCameraMove() {
        Vector3 cameraPos = this.transform.position + Vector3.forward * 10;
        Vector3 followPos = playerScript.transform.position;

        Vector3 dist = followPos - cameraPos;


        this.transform.position += dist * 0.5f * Time.deltaTime * 5;
    }
    void EventCameraMove() {
      

        Vector3 dist = eventPos - this.transform.position;


        this.transform.position += dist * 0.5f * Time.deltaTime * 5;
    }
    public void SetEventPos(Vector3 pos) {
        eventPos = pos;
    }

}
