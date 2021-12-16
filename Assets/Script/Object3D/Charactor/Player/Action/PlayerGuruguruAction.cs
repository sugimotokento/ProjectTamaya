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
    Vector3 positionBuffer;

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


        //線の太さを設定
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
            player.sound.PlayShot(PlayerSound.SoundIndex.GURUGURU);
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

            StageManager.instance.camera.SetAction<PlayerCameraAction>();

            
            player.visual.transform.localPosition = Vector3.zero;
            player.visual.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            player.ChangeAction<PlayerGuruguruAction, PlayerMoveAction>();
            positionBuffer.z = 0;
            player.transform.position = positionBuffer;

            //グルグル失敗
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
        //グルグル出来てる時と出来てない時で色を変える
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