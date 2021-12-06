using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

    public static StageManager instance = null;
    public static string sceneName;

    public GameObject eventObj;
    public Player player;
    public GameCamera camera;
    public PauseManager pause;

    [HideInInspector] public bool isClear = false;
    [HideInInspector] public bool isEventActive = false;

    private List<GameEvent> gameEvents=new List<GameEvent>();
    private bool canSetPlayerAction = false;

    private void Awake() {
        instance = this;

        for(int i=0; i < eventObj.transform.childCount; ++i) {
            gameEvents.Add(eventObj.transform.GetChild(i).gameObject.GetComponent<GameEvent>());
        }
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
        isEventActive = false;
        for (int i = 0; i < gameEvents.Count; ++i) {
            if (gameEvents[i].GetIsEvent() == true) {
                gameEvents[i].SetCanEvent(true);
                canSetPlayerAction = true;
                player.SetActiveUI(false);
                player.ReMoveActionAll();
                isEventActive = true;
                break;
            }
        }

        if (isEventActive == false) {
            if (canSetPlayerAction == true) {
                player.SetActiveUI(true);
                player.SetDefaultAction();
                canSetPlayerAction = false;
            }
        }
    }
}
