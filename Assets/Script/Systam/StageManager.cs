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

    public AudioClip startBGMClip;
    public AudioClip mainBGMClip;
    public AudioClip battleBGMClip;
    public AudioClip eventBGMClip;
    public AudioSource bgm;
    public AudioSource environment;

    [SerializeField] private FadeOut2 fade;
    [SerializeField] private string nextSceneName;

    [HideInInspector] public bool isClear = false;
    [HideInInspector] public bool isEventActive = false;


    private GameObject enemyManagerObj;
    private List<Enemy> enemyScript = new List<Enemy>();

    public bool canMainBGM = false;
    private bool canBattleBGM = true;

    private List<GameEvent> gameEvents = new List<GameEvent>();
    private bool canSetPlayerAction = false;

    private void Awake() {
        instance = this;

        for (int i = 0; i < eventObj.transform.childCount; ++i) {
            gameEvents.Add(eventObj.transform.GetChild(i).gameObject.GetComponent<GameEvent>());
        }

        enemyManagerObj = GameObject.Find("EnemyManager");
        for (int i = 0; i < enemyManagerObj.transform.childCount; ++i) {
            enemyScript.Add(enemyManagerObj.transform.GetChild(i).gameObject.GetComponent<Enemy>());
        }
    }

    // Start is called before the first frame update
    void Start() {
        sceneName = SceneManager.GetActiveScene().name;
        player.SetDefaultAction();

        bgm.clip = startBGMClip;
        bgm.Play();
    }

    // Update is called once per frame
    void Update() {

        ChangeBGM();
        Event();
    }

    private void Event() {
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
    private void ChangeBGM() {


        //�o�g������
        bool isBattleMode = false;
        for (int i = 0; i < enemyScript.Count; ++i) {
            if (enemyScript[i].isSumaki==true) {
                enemyScript[i].SCENE_NUM = 1;
            }
        }
        for (int i = 0; i < enemyScript.Count; ++i) {
            if (enemyScript[i].SCENE_NUM == 3) {
                isBattleMode = true;
                break;
            }
        }


        //�X�^�[�gBGM���烁�C��BGM
        if ( isBattleMode==false) {
            if (canMainBGM == true) {
                bgm.Stop();
                bgm.clip = mainBGMClip;
                bgm.Play();
                canMainBGM = false;
            }
        } else {
            canMainBGM = true;
        }


        //�o�g��BGM�Đ�
        if (isBattleMode == true && canBattleBGM == true) {
            bgm.Stop();
            bgm.clip = battleBGMClip;
            bgm.Play();
            canBattleBGM = false;
        }
        if (isBattleMode == false) {
            canBattleBGM = true;
        }


        if (isClear == true) {
            bgm.Stop();
        }
    }

    public void SetNextScene() {
        fade.gameObject.SetActive(true);
        if (fade.GetIsFadeEnd() == true) {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
