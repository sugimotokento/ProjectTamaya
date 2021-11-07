using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public static StageManager instance = null;

    public GameEvent startEvent;
    public Player player;
    public GameCamera camera;
    public GameObject enemy;

    private bool canSetPlayerAction = false;

    private void Awake() {
        instance = this;

        if (startEvent == null) {
            player.SetDefaultAction();
        } else {
            startEvent.SetIsEvent(true);
            canSetPlayerAction = true;
        }
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (startEvent != null && startEvent.GetIsEvent() == true) {

            canSetPlayerAction = true;
            player.ReMoveActionAll();
        } else {
            if (canSetPlayerAction == true) {
                player.SetDefaultAction();
                canSetPlayerAction = false;
            }
        }
    }
}
