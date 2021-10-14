using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuruguruAction : PlayerAction {
    enum LineIndex {
        ROLL,
        JOIN,
        MAX
    };



    GameObject target;

    LineRenderer[] line = new LineRenderer[(int)LineIndex.MAX];

    List<Vector3> linePos = new List<Vector3>();

    Vector3 mousePosBuffer;
    Vector3 initTargetDist;
    Vector3 guruguruCrossBuffer;

    float timer = 0;

    const int LINE_MAX = 10;
    int count = 0;

    bool isGuruguru = true;
    bool isEnd = false;
    bool isRightMouseDown = false;



    public override void Init(Player p) {
        base.Init(p);

        //Line�̏�����
        line[(int)LineIndex.ROLL] = player.guruguruLine[(int)LineIndex.ROLL].GetComponent<LineRenderer>();
        line[(int)LineIndex.JOIN] = player.guruguruLine[(int)LineIndex.JOIN].GetComponent<LineRenderer>();
        line[(int)LineIndex.ROLL].positionCount = LINE_MAX;
        line[(int)LineIndex.JOIN].positionCount = 2;
        for (int i = 0; i < LINE_MAX; ++i) {
            linePos.Add(GetWorldMousePos());
            line[(int)LineIndex.ROLL].SetPosition(i, linePos[i]);
        }

        //Line��\��
        for (int i = 0; i < (int)LineIndex.MAX; ++i) {
            player.guruguruLine[i].SetActive(true);
        }
    }

    public override void UpdateAction() {
        if (Input.GetMouseButtonDown(1)) {
            isRightMouseDown = true;
        }
    }

    public override void Action() {
        Line();
        Guruguru();
        GuruguruCount();
        GuruguruAnimatioin();
        GuruguruEnd();
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

        if (isGuruguru == true) {
            for (int i = 0; i < (int)LineIndex.MAX; ++i) {
                line[i].materials[0].color = Color.green;
                line[i].materials[0].SetColor("_EmissionColor", Color.green);
            }
        } else {
            for (int i = 0; i < (int)LineIndex.MAX; ++i) {
                line[i].materials[0].color = Color.red;
                line[i].materials[0].SetColor("_EmissionColor", Color.red);
            }
        }
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

        }
        Debug.Log(count);

        guruguruCrossBuffer = Vector3.Cross(dist2, dist1);
    }
    private void GuruguruEnd() {
        timer += Time.fixedDeltaTime;
        if (timer > 7) {
            isEnd = true;
        }

        if (isRightMouseDown == true) {
            isEnd = true;
        }


        if (count >= 8) {
            isEnd = true;
        }

        //�U�����󂯂Ă�����


        if (isEnd == true) {
            for (int i = 0; i < (int)LineIndex.MAX; ++i) {
                player.guruguruLine[i].SetActive(false);
            }
            player.visual.transform.localPosition = Vector3.zero;
            player.ChangeAction<PlayerGuruguruAction, PlayerMoveAction>();
        }
    }
    private void GuruguruAnimatioin() {
        Vector3 mouseDist = target.transform.position - GetWorldMousePos();
        float rad = Mathf.Atan2(mouseDist.y, mouseDist.x);

        player.visual.transform.position = target.transform.position + new Vector3(-Mathf.Cos(rad), -Mathf.Sin(rad), 0) * 2;
    }

    private void Line() {
        // �擪�ɑ}��
        linePos.RemoveAt(linePos.Count - 1);
        linePos.Insert(0, GetWorldMousePos());

        for (int i = 0; i < linePos.Count; ++i) {
            line[(int)LineIndex.ROLL].SetPosition(i, linePos[i]);
        }


        //�v���C���[�ƃG�l�~�[���Ȃ����
        line[(int)LineIndex.JOIN].SetPosition(0, target.transform.position);
        line[(int)LineIndex.JOIN].SetPosition(1, player.visual.transform.position);

    }

    public void SetTartegt(GameObject obj) {
        target = obj;
        initTargetDist = target.transform.position - GetWorldMousePos();
    }
}