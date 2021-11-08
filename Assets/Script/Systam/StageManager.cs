using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

    public static StageManager instance = null;
    public static string sceneName;

    public GameEvent[] gameEvent = new GameEvent[2];
    public Player player;
    public GameCamera camera;
    public GameObject enemy;

    private bool canSetPlayerAction = false;

    private void Awake() {
        instance = this;

    }

    // Start is called before the first frame update
    void Start() {
        sceneName = SceneManager.GetActiveScene().name;
        player.SetDefaultAction();
    }

    // Update is called once per frame
    void Update() {

        Event();
    }

    private void Event(){
        bool isActiveEvent = false;
        for (int i = 0; i < gameEvent.Length; ++i) {
            if (gameEvent[i].GetIsEvent() == true) {
                gameEvent[i].SetCanEvent(true);
                canSetPlayerAction = true;
                player.SetActiveUI(false);
                player.ReMoveActionAll();
                isActiveEvent = true;
                break;
            }
        }

        if (isActiveEvent == false) {
            if (canSetPlayerAction == true) {
                player.SetActiveUI(true);
                player.SetDefaultAction();
                canSetPlayerAction = false;
            }
        }
    }
}
