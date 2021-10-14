using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuruguruAction : PlayerAction {
    GameObject target;

    LineRenderer lineRenderer;

    List<Vector3> linePos = new List<Vector3>();

    Vector3 mousePosBuffer;
    Vector3 initTargetDist;
    Vector3 guruguruCrossBuffer;

    const int LINE_MAX = 10;
    int count = 0;

    bool isGuruguru = true;

    public override void Init(Player p) {
        base.Init(p);
        target = player.gameObject;
        initTargetDist = target.transform.position - GetWorldMousePos();

        //Lineの初期化
        lineRenderer = player.guruguruLine.GetComponent<LineRenderer>();
        lineRenderer.positionCount = LINE_MAX;
        for (int i=0; i< LINE_MAX; ++i) {
            linePos.Add(GetWorldMousePos());
            lineRenderer.SetPosition(i, linePos[i]);
        }

        player.guruguruLine.SetActive(true);
    }
    public override void Action() {
        Line();
        Guruguru();
        GuruguruCount(); 
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
            count = 0;
        }
        mousePosBuffer = GetWorldMousePos();

        if (isGuruguru == true) {
            lineRenderer.materials[0].color = Color.green;
            lineRenderer.materials[0].SetColor("_EmissionColor", Color.green);
        } else {
            lineRenderer.materials[0].color = Color.red;
            lineRenderer.materials[0].SetColor("_EmissionColor", Color.red);
        }
    }
    private void GuruguruCount() {
        //初期マウス座標と今のマウスの座標の距離
        Vector3 dist1 = GetWorldMousePos() - (initTargetDist+target.transform.position);

        //初期マウス座標とターゲットとの距離
        Vector3 dist2 = player.transform.position - (initTargetDist + target.transform.position);

        //1フレーム前の外積の結果がーで今の外積の結果が＋なら1回転
        Vector3 cross = Vector3.Cross(dist2, dist1);
        if(guruguruCrossBuffer.z<0 && cross.z > 0 && isGuruguru==true) {
            count++;
        
        }
        Debug.Log(count);

        guruguruCrossBuffer= Vector3.Cross(dist2, dist1);
    }

    private void Line() {
        // 先頭に挿入
        linePos.RemoveAt(linePos.Count - 1);
        linePos.Insert(0, GetWorldMousePos());

        for (int i = 0; i < linePos.Count; ++i) {
            lineRenderer.SetPosition(i, linePos[i]);
        }
    }

    public void SetTartegt(GameObject obj) {
        target = obj;
    }
}