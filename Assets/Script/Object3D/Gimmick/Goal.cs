using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    [HideInInspector] public bool isGoal = false;
    [SerializeField] private Renderer renderer;
    [SerializeField] private GameObject bloomObject;
    [SerializeField] private GameObject steam;
    [SerializeField] private GameObject playerPosObj;

    [SerializeField] private ParticleSystem thunder;
    [SerializeField] private ParticleSystem spark;

    private Color baseColor;
    private Vector3 mousePosBuffer;
    private Vector3 baseScele;
    float rotationSpeed = 0;
    float flashTimer = 0;
    float animationIntervalTimer = 0;
    float ClearInterval = 0;
    private bool isGuruguru = false;
    private bool isFlash = false;
    private bool isAnimationEnd = false;
    private bool canClear = false;

    // Start is called before the first frame update
    void Start() {
        baseColor = renderer.material.GetColor("_EmissionColor");
        baseScele = bloomObject.transform.localScale;

        var thunderEmission = thunder.emission;
        var sparkEmission = thunder.emission;
        thunderEmission.rateOverTime = rotationSpeed * 100 + 40;
        sparkEmission.rateOverTime = Mathf.Max(0, rotationSpeed * 100 - 50);
    }

    private void Update() {
        animationIntervalTimer += Time.deltaTime;
        if (animationIntervalTimer < 2) return;
        bloomObject.transform.Rotate(Vector3.up * 100 * rotationSpeed * Time.deltaTime);

        if (isGoal == true && isAnimationEnd == false) {
            Gruguru();

            if (isGuruguru == true && isFlash == false) {
                steam.SetActive(false);
                rotationSpeed +=Time.deltaTime;

                var thunderEmission = thunder.emission;
                var sparkEmission = thunder.emission;
                thunderEmission.rateOverTime = rotationSpeed* 100 + 40;
                sparkEmission.rateOverTime =Mathf.Max(0, rotationSpeed* 100 - 50);

                //���o�I��
                if (rotationSpeed > 3.5f) {
                    canClear = true;
                    isFlash = true;
                }
            } else {
                rotationSpeed -= Time.deltaTime;
                if (rotationSpeed < 0) {
                    rotationSpeed = 0;
                }
            }


            //���̏�Ԃɖ߂�
            if (isFlash == true) {
                flashTimer += Time.deltaTime*0.5f;
                steam.SetActive(true);
                renderer.material.SetColor("_EmissionColor", baseColor * Mathf.Pow(Mathf.Sin(flashTimer * Mathf.PI) * 10, 3));
                //bloomObject.transform.localScale += new Vector3(1, 1, 1) * Time.fixedDeltaTime * 16;

                if (flashTimer > 1) {
                    steam.SetActive(true);
                   
                    renderer.material.SetColor("_EmissionColor", baseColor * Mathf.Pow(1, 2));
                    isAnimationEnd = true;
                }
            }
        }

        if (canClear == true) {
            ClearInterval += Time.deltaTime;
            if (ClearInterval > 2.5f) {
                StageManager.instance.isClear = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Player player = other.gameObject.GetComponent<Player>();

            //�X�R�A�ݒ�
            ScoreManager.SetAir(player.air.GetTimer());
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player") && isFlash == false) {
            Player player = other.gameObject.GetComponent<Player>();

            //�v���C���[�̓������~�߂�
            player.gameObject.transform.position = playerPosObj.transform.position;

            //�����J�����ɕύX
            StageManager.instance.camera.SetAction<PointCameraAction>();
            StageManager.instance.camera.GetAction<PointCameraAction>().SetPoint(this.transform.position - Vector3.forward * 7);//7

            isGoal = true;
        }
    }

    private void Gruguru() {
        Vector3 mousePos = GetWorldMousePos();


        //�ڕW�ƃ}�E�X���W������
        Vector3 dist1 = this.transform.position - mousePos;

        //1�t���[���O�̃}�E�X���W�ƍ��̃}�E�X���W�̋���
        Vector3 dist2 = mousePosBuffer - mousePos;

        //�l��+�������玞�v���
        Vector3 cross = Vector3.Cross(dist2, dist1);
        if (cross.z > 0) {
            //�ǂ�����
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
