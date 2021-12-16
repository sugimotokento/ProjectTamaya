using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuruguruAction : PlayerAction {
    enum LineIndex {
        MOUSE,
        JOIN,
        MAX
    };

    GameObject target;
    GameObject rope;//�~���I�u�W�F�N�g

    LineRenderer[] line = new LineRenderer[(int)LineIndex.MAX];

    List<Vector3> linePos = new List<Vector3>();

    Vector3 mousePosBuffer;
    Vector3 initTargetDist;
    Vector3 guruguruCrossBuffer;
    Vector3 positionBuffer;

    float timer = 0;

    const int LINE_MAX = 10;
    int count = 0;

    bool isGuruguru = true;
    bool isEnd = false;
    bool isRightMouseDown = false;

    public override void Init(Player p) {
        base.Init(p);

        //Line�̏�����
        line[(int)LineIndex.MOUSE] = player.line[(int)LineIndex.MOUSE].GetComponent<LineRenderer>();
        line[(int)LineIndex.JOIN] = player.line[(int)LineIndex.JOIN].GetComponent<LineRenderer>();
        line[(int)LineIndex.MOUSE].positionCount = LINE_MAX;
        line[(int)LineIndex.JOIN].positionCount = 2;
        for (int i = 0; i < LINE_MAX; ++i) {
            linePos.Add(GetWorldMousePos());
            line[(int)LineIndex.MOUSE].SetPosition(i, linePos[i]);
        }

        //Line��\��
        for (int i = 0; i < (int)LineIndex.MAX; ++i) {
            player.line[i].SetActive(true);
        }


        //���̑�����ݒ�
        line[(int)LineIndex.MOUSE].startWidth = 0.1f;
        line[(int)LineIndex.MOUSE].endWidth = 0.1f;

        line[(int)LineIndex.JOIN].startWidth = 0.05f;
        line[(int)LineIndex.JOIN].endWidth = 0.05f;

    }

    public override void UpdateAction() {
        if (Input.GetMouseButtonDown(1)) {
            isRightMouseDown = true;
        }
    }

    public override void Action() {
       

        Guruguru();
        GuruguruCount();
        GuruguruAnimatioin();
        GuruguruEnd();
        ChangeColor();
        Line();
        isRightMouseDown = false;

       
    }


    private void Guruguru() {
        Vector3 mousePos = GetWorldMousePos();


        //�ڕW�ƃ}�E�X���W������
        Vector3 dist1 = target.transform.position - mousePos;

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
        mousePosBuffer = GetWorldMousePos();


       
    }
    private void GuruguruCount() {
        //�����}�E�X���W�ƍ��̃}�E�X�̍��W�̋���
        Vector3 dist1 = GetWorldMousePos() - (initTargetDist + target.transform.position);

        //�����}�E�X���W�ƃ^�[�Q�b�g�Ƃ̋���
        Vector3 dist2 = player.transform.position - (initTargetDist + target.transform.position);

        //1�t���[���O�̊O�ς̌��ʂ��[�ō��̊O�ς̌��ʂ��{�Ȃ�1��]
        Vector3 cross = Vector3.Cross(dist2, dist1);
        if (guruguruCrossBuffer.z < 0 && cross.z > 0 && isGuruguru == true) {
            count++;
            player.sound.PlayShot(PlayerSound.SoundIndex.GURUGURU);
        }

        guruguruCrossBuffer = Vector3.Cross(dist2, dist1);
    }
    private void GuruguruEnd() {
        //7�b�o�߂ŏI��
        timer += Time.fixedDeltaTime;
        if (timer > 7) {
            isEnd = true;
        }

        //�E�N���b�N�ŏI��
        if (isRightMouseDown == true) {
            isEnd = true;
        }

        //10��]�ŏI��
        if (count >= 10) {
            isEnd = true;
        } 

        //�U�����󂯂Ă�����


        //�O���O���I��
        if (isEnd == true) {
            for (int i = 0; i < (int)LineIndex.MAX; ++i) {
                player.line[i].SetActive(false);
            }

            StageManager.instance.camera.SetAction<PlayerCameraAction>();

            
            player.visual.transform.localPosition = Vector3.zero;
            player.visual.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            player.ChangeAction<PlayerGuruguruAction, PlayerMoveAction>();
            positionBuffer.z = 0;
            player.transform.position = positionBuffer;

            //�O���O�����s
            if (count < 10) {
                Destroy(rope);
                target.GetComponent<Enemy>().isSumaki = false;
            }
        }
    }
    private void GuruguruAnimatioin() {
        Vector3 mouseDist = target.transform.position - GetWorldMousePos();
        float rad = Mathf.Atan2(mouseDist.y, mouseDist.x);

        player.visual.transform.position = target.transform.position + new Vector3(-Mathf.Cos(rad), -Mathf.Sin(rad), 0) * 2;
        player.visual.transform.rotation = Quaternion.AngleAxis(rad/3.14f*180+180, Vector3.forward);
    }

    private void ChangeColor() {
        //�O���O���o���Ă鎞�Əo���ĂȂ����ŐF��ς���
        if (isGuruguru == true) {
            for (int i = 0; i < (int)LineIndex.MAX; ++i) {
                line[i].materials[0].color = Color.green;
                line[i].materials[0].SetColor("_EmissionColor", Color.green*20);
            }
        } else {
            for (int i = 0; i < (int)LineIndex.MAX; ++i) {
                line[i].materials[0].color = Color.red;
                line[i].materials[0].SetColor("_EmissionColor", Color.red*20);
            }
        }
    }
    private void Line() {
        //�}�E�X�J�[�\���̋O������ŕ`��
        // �擪�ɑ}��
        linePos.RemoveAt(linePos.Count - 1);
        linePos.Insert(0, GetWorldMousePos());

        for (int i = 0; i < linePos.Count; ++i) {
            line[(int)LineIndex.MOUSE].SetPosition(i, linePos[i]);
        }


        //�v���C���[�ƃG�l�~�[���Ȃ����
        line[(int)LineIndex.JOIN].SetPosition(0, target.transform.position);
        line[(int)LineIndex.JOIN].SetPosition(1, player.visual.transform.position);


        //�G���O���O�������ɂ���
        if (count >= 8) {
            rope.transform.GetChild(0).gameObject.SetActive(true);
        } else if (count >= 6) {
            rope.transform.GetChild(1).gameObject.SetActive(true);
        } else if (count >= 3) {
            rope.transform.GetChild(2).gameObject.SetActive(true);
        }

    }

    public void SetTartegt(GameObject obj) {
        target = obj;
        initTargetDist = target.transform.position - GetWorldMousePos();
        rope = Instantiate(player.rope, target.transform.position, Quaternion.identity);


        positionBuffer = target.transform.position - player.moveSpeed.normalized;
        player.transform.position = obj.transform.position;
        player.transform.position -= Vector3.back * 4;

        StageManager.instance.camera.SetAction<PointCameraAction>();
        StageManager.instance.camera.GetAction<PointCameraAction>().SetPoint(target.gameObject.transform.position + Vector3.forward * -5);

        target.GetComponent<Enemy>().isSumaki = true;
        rope.transform.rotation = target.transform.GetChild(0).gameObject.transform.rotation;
    }

    public GameObject GetTarget() {
        return target;
    }
}