using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Vector3 playerPos = StageManager.instance.player.transform.position;
        Vector3 cameraPos = this.transform.position+Vector3.back*-10;

        Vector3 dist = playerPos - cameraPos;


        this.transform.position += dist * 0.5f*Time.deltaTime*5;

    }
}
