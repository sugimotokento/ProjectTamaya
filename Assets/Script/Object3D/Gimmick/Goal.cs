using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    [HideInInspector] public bool isGoal = false;
    [SerializeField] private Renderer renderer;
    [SerializeField] private GameObject bloomObject;

    private Color baseColor;
    private Vector3 mousePosBuffer;
    private Vector3 baseScele;
    float timer = 0;
    private bool isGuruguru = false;
    private bool isRevert = false;
    private bool isAnimationEnd = false;
    // Start is called before the first frame update
    void Start() {
        baseColor = renderer.material.GetColor("_EmissionColor");
        baseScele = bloomObject.transform.localScale;
    }

    private void FixedUpdate() {
        if (isGoal == true && isAnimationEnd == false) {
            Gruguru();

            if (isGuruguru == true && isRevert == false) {
                renderer.material.SetColor("_EmissionColor", baseColor * Mathf.Pow(timer, 2));
                bloomObject.transform.localScale += new Vector3(1, 1, 0) * Time.fixedDeltaTime * 2;
                timer += Time.fixedDeltaTime * 5;

                //���o�I��
                if (timer > 10) {
                    StageManager.instance.isClear = true;
                    isRevert = true;
                }
            }


            //���̏�Ԃɖ߂�
            if (isRevert == true) {
                renderer.material.SetColor("_EmissionColor", baseColor * Mathf.Pow(timer, 2));
                bloomObject.transform.localScale -= new Vector3(1, 1, 0) * Time.fixedDeltaTime * 2;
                timer -= Time.fixedDeltaTime * 5;

                if (timer <= 1) {
                    renderer.material.SetColor("_EmissionColor", baseColor * Mathf.Pow(1, 2));
                    bloomObject.transform.localScale = baseScele;
                    isAnimationEnd = true;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Player player = other.gameObject.GetComponent<Player>();

            //�v���C���[�̓������~�߂�
            player.ReMoveActionAll();
            player.gameObject.transform.position = this.transform.position;

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