using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLandmineAction : BossAction {
    private const float SHOT_INTERVAL_BASE = 0.4f;
    private const float SHOT_INTERVAL_LATE = 0.3f;//Ç±Ç±Ç≈íeÇÃèoÇÈó Çí≤êÆ
    private const float ACTION_TIME = 5;
    private const float ROTATE_SPEED_MAX = 700;
    private float shotIntervalTimer = 0;
    private float actionTimer = 0;

    private List<GameObject> mine = new List<GameObject>();
    private List<Vector3> point = new List<Vector3>();
    private List<float> moveLate = new List<float>();
    private List<float> destroyTimer = new List<float>();

    public override void Init(Boss b) {
        base.Init(b);
        boss.animator.SetBool("isShot", true);
    }

    public override void Action() {
        ShotLandmine();
        Rotation();

        if(actionTimer / ACTION_TIME>=1){
            boss.SetAction<BossIdleAction>();
            
            for(int i=0; i<mine.Count; i++) {
                mine[i].GetComponent<BossBullet>().isObjectMode = false;
            }

            boss.animator.SetBool("isShot", false);
        }

        if (boss.isDamage == true) {
            if (boss.hp == 3) {
                boss.SetAction<BossAngerAction>();
            } else {
                boss.SetAction<BossDamageAction>();
            }
        }
    }

    private void ShotLandmine() {
        shotIntervalTimer += Time.fixedDeltaTime;

        float late = actionTimer / ACTION_TIME;
        late = Mathf.Sin(late * 3.14f);

        if (shotIntervalTimer > SHOT_INTERVAL_BASE - late* SHOT_INTERVAL_LATE) {
            float range = 14;
            boss.sound.Play(BossSound.SoundIndex.SHOT);
            GameObject obj = Instantiate(boss.bullet);
            BossBullet script = obj.GetComponent<BossBullet>();
            script.SetMoveSpeed(0);
            script.isObjectMode = true;

            mine.Add(obj);
            point.Add(new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0));
            moveLate.Add(0);
            destroyTimer.Add(0);

            shotIntervalTimer = 0;
        }

        //íeÇÃçÌèú
        for (int i = 0; i < mine.Count; ++i) {
            if(mine[i].GetComponent<BossBullet>().isHit == true) {
                Destroy(mine[i].gameObject);
                mine.RemoveAt(i);
                moveLate.RemoveAt(i);
                destroyTimer.RemoveAt(i);
                point.RemoveAt(i);

            }else if (mine[i].GetComponent<BossBullet>().isDestroy == true) {
                Destroy(mine[i].gameObject);
                mine.RemoveAt(i);
                moveLate.RemoveAt(i);
                destroyTimer.RemoveAt(i);
                point.RemoveAt(i);

            } else if (moveLate[i] > 1) {
                destroyTimer[i] += Time.fixedDeltaTime;
                if (destroyTimer[i] > 2) {
                    Destroy(mine[i].gameObject);
                    mine.RemoveAt(i);
                    moveLate.RemoveAt(i);
                    destroyTimer.RemoveAt(i);
                    point.RemoveAt(i);

                }
            }
        }

        //äeíeÇÃèàóù
        for (int i = 0; i < mine.Count; ++i) {
            //ìÆÇ©Ç∑
            mine[i].gameObject.transform.position = Vector3.Lerp(boss.gameObject.transform.position, point[i], moveLate[i]);
            moveLate[i] += Time.fixedDeltaTime* (1+late)*0.5f;
        }
    }
    private void Rotation() {
        actionTimer += Time.fixedDeltaTime;
        float late = actionTimer / ACTION_TIME;
        late = Mathf.Sin(late * 3.14f);
        float rotateSpeed = late* ROTATE_SPEED_MAX;

        boss.transform.Rotate(Vector3.forward * rotateSpeed*Time.fixedDeltaTime);
    }
}
