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
    GameObject rope;//円柱オブジェクト

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

        //Lineの初期化
        line[(int)LineIndex.MOUSE] = player.line[(int)LineIndex.MOUSE].GetComponent<LineRenderer>();
        line[(int)LineIndex.JOIN] = player.line[(int)LineIndex.JOIN].GetComponent<LineRenderer>();
        line[(int)LineIndex.MOUSE].positionCount = LINE_MAX;
        line[(int)LineIndex.JOIN].positionCount = 2;
        for (int i = 0; i < LINE_MAX; ++i) {
            linePos.Add(GetWorldMousePos());
            line[(int)LineIndex.MOUSE].SetPosition(i, linePos[i]);
        }

        //Lineを表示
        for (int i = 0; i < (int)LineIndex.MAX; ++i) {
            player.line[i].SetActive(true);
        }
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


        //目標とマウス座標を距離
        Vector3 dist1 = target.transform.position - mousePos;

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
        mousePosBuffer = GetWorldMousePos();


       
    }
    private void GuruguruCount() {
        //初期マウス座標と今のマウスの座標の距離
        Vector3 dist1 = GetWorldMousePos() - (initTargetDist + target.transform.position);

        //初期マウス座標とターゲットとの距離
        Vector3 dist2 = player.transform.position - (initTargetDist + target.transform.position);

        //1フレーム前の外積の結果がーで今の外積の結果が＋なら1回転
        Vector3 cross = Vector3.Cross(dist2, dist1);
        if (guruguruCrossBuffer.z < 0 && cross.z > 0 && isGuruguru == true) {
            count++;

        }

        guruguruCrossBuffer = Vector3.Cross(dist2, dist1);
    }
    private void GuruguruEnd() {
        //7秒経過で終了
        timer += Time.fixedDeltaTime;
        if (timer > 7) {
            isEnd = true;
        }

        //右クリックで終了
        if (isRightMouseDown == true) {
            isEnd = true;
        }

        //10回転で終了
        if (count >= 10) {
            isEnd = true;
        }

        //攻撃を受けても解除


        //グルグル終了
        if (isEnd == true) {
            for (int i = 0; i < (int)LineIndex.MAX; ++i) {
                player.line[i].SetActive(false);
            }
            //Destroy(rope);
            player.visual.transform.localPosition = Vector3.zero;
            player.visual.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            player.ChangeAction<PlayerGuruguruAction, PlayerMoveAction>();
        }
    }
    private void GuruguruAnimatioin() {
        Vector3 mouseDist = target.transform.position - GetWorldMousePos();
        float rad = Mathf.Atan2(mouseDist.y, mouseDist.x);

        player.visual.transform.position = target.transform.position + new Vector3(-Mathf.Cos(rad), -Mathf.Sin(rad), 0) * 2;
        player.visual.transform.rotation = Quaternion.AngleAxis(rad/3.14f*180+180, Vector3.forward);
    }

    private void ChangeColor() {
        //グルグル出来てる時と出来てない時で色を変える
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
    private void Line() {
        //マウスカーソルの軌道を線で描画
        // 先頭に挿入
        linePos.RemoveAt(linePos.Count - 1);
        linePos.Insert(0, GetWorldMousePos());

        for (int i = 0; i < linePos.Count; ++i) {
            line[(int)LineIndex.MOUSE].SetPosition(i, linePos[i]);
        }


        //プレイヤーとエネミーをつなげる線
        line[(int)LineIndex.JOIN].SetPosition(0, target.transform.position);
        line[(int)LineIndex.JOIN].SetPosition(1, player.visual.transform.position);


        //敵をグルグル巻きにする
        if (count >= 8) {
            rope.transform.localScale = new Vector3(rope.transform.localScale.x, 0.4f, rope.transform.localScale.z);
        } else if (count >= 6) {
            rope.transform.localScale = new Vector3(rope.transform.localScale.x, 0.28f, rope.transform.localScale.z);
        } else if (count >= 3) {
            rope.transform.localScale = new Vector3(rope.transform.localScale.x, 0.15f, rope.transform.localScale.z);
        }

    }

    public void SetTartegt(GameObject obj) {
        target = obj;
        initTargetDist = target.transform.position - GetWorldMousePos();
        rope = Instantiate(player.rope, target.transform.position, Quaternion.identity);
        rope.transform.localScale = new Vector3(rope.transform.localScale.x, 0.08f, rope.transform.localScale.z);
    }
}