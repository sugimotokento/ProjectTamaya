using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuruguruAction : PlayerAction {
    GameObject target;

    Vector3 mousePosBuffer;
    Vector3 initTargetDist;
    Vector3 guruguruCrossBuffer;
    ParticleSystem particle;

    int count = 0;

    bool isGuruguru = true;
    public override void Init(Player p) {
        base.Init(p);
        target = player.gameObject;
        initTargetDist = target.transform.position - GetWorldMousePos();
    }
    public override void Action() {
        Guruguru();
        GuruguruCoutn();
        mousePosBuffer = GetWorldMousePos();
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

        GameObject obj = (Instantiate(player.guruguruParticle, mousePos, Quaternion.identity));
        if (isGuruguru == true) {
            ParticleSystem.MainModule par = obj.GetComponent<ParticleSystem>().main;
            par.startColor = Color.blue;
        } else {
            ParticleSystem.MainModule par = obj.GetComponent<ParticleSystem>().main;
            par.startColor = Color.red;
        }

       
    }
    private void GuruguruCoutn() {
        Vector3 dist1 = GetWorldMousePos() - (initTargetDist+target.transform.position);
        Vector3 dist2 = player.transform.position - (initTargetDist + target.transform.position);
        Vector3 cross = Vector3.Cross(dist2, dist1);

        if(guruguruCrossBuffer.z<0 && cross.z > 0 && isGuruguru==true) {
            count++;
        }
        Debug.Log(count);

        guruguruCrossBuffer= Vector3.Cross(dist2, dist1);
    }
}
