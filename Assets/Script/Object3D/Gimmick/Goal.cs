using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    [HideInInspector] public bool isGoal = false;
    [SerializeField] private Renderer renderer;
    [SerializeField] private GameObject bloomObject;
    [SerializeField] private GameObject steam;

    private Color baseColor;
    private Vector3 mousePosBuffer;
    private Vector3 baseScele;
    float timer = 0;
    float flashTimer = 0;
    private bool isGuruguru = false;
    private bool isFlash = false;
    private bool isAnimationEnd = false;

    // Start is called before the first frame update
    void Start() {
        baseColor = renderer.material.GetColor("_EmissionColor");
        baseScele = bloomObject.transform.localScale;
    }

    private void Update() {
        if (isGoal == true && isAnimationEnd == false) {
            Gruguru();

            if (isGuruguru == true && isFlash == false) {
                steam.SetActive(false);
                timer += Time.deltaTime;
                bloomObject.transform.Rotate(Vector3.up * 100* timer * Time.deltaTime);
                //演出終了
                if (timer > 3) {

                    isFlash = true;
                }
            }


            //元の状態に戻す
            if (isFlash == true) {
                flashTimer += Time.deltaTime;
                steam.SetActive(true);
                renderer.material.SetColor("_EmissionColor", baseColor * Mathf.Pow(Mathf.Sin(flashTimer * Mathf.PI) * 20, 3));
                //bloomObject.transform.localScale += new Vector3(1, 1, 1) * Time.fixedDeltaTime * 16;

                if (flashTimer > 1) {
                    steam.SetActive(true);
                    StageManager.instance.isClear = true;
                    renderer.material.SetColor("_EmissionColor", baseColor * Mathf.Pow(1, 2));
                    bloomObject.transform.localScale = baseScele;
                    isAnimationEnd = true;
                }
            }
        }

        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Player player = other.gameObject.GetComponent<Player>();

            //スコア設定
            ScoreManager.SetAir(player.air.GetTimer());
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player") && isFlash == false) {
            Player player = other.gameObject.GetComponent<Player>();

            //プレイヤーの動きを止める
            player.ReMoveActionAll();
            player.gameObject.transform.position = this.transform.position;

            //注視カメラに変更
            StageManager.instance.camera.SetAction<PointCameraAction>();
            StageManager.instance.camera.GetAction<PointCameraAction>().SetPoint(this.transform.position - Vector3.forward * 7);//7

            isGoal = true;
        }
    }

    private void Gruguru() {
        Vector3 mousePos = GetWorldMousePos();


        //目標とマウス座標を距離
        Vector3 dist1 = this.transform.position - mousePos;

        //1フレーム前のマウス座標と今のマウス座標の距離
        Vector3 dist2 = mousePosBuffer - mousePos;

        //値が+だったら時計回り
        Vector3 cross = Vector3.Cross(dist2, dist1);
        if (cross.z > 0) {
            //良い感じ
            isGuruguru = true;

        } else {
            isGuruguru = false;
        }
        if ((mousePos - mousePosBuffer).magnitude < 0.01f) {
            isGuruguru = false;
        }

        mousePosBuffer = GetWorldMousePos();
    }
    private Vector3 GetWorldMousePos() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        return mousePos;
    }
}
