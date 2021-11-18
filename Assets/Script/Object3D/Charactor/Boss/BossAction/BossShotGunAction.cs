using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotGunAction : BossAction {
    private const float SHOT_INTERVAL = 0.1f;
    private const float SHOT_TIME = 2;

    private LineRenderer line;

    private int shotCount = 0;

    private float shotIntervalTimer = 0;
    private float shotTimer = 0;


    public override void Init(Boss b) {
        base.Init(b);

        boss.line.SetActive(true);
        line = boss.line.GetComponent<LineRenderer>();
        line.positionCount = 2;

        line.startWidth = 0.1f;
        line.endWidth = 0.1f;

        line.materials[0].color = Color.blue;
        line.materials[0].SetColor("_EmissionColor", Color.blue * 20);
    }

    public override void Action() {
        LockAtPlayer();
        Shot();
    }

    private void LockAtPlayer() {
        Player player = StageManager.instance.player;

        //Ç¢ÇÎÇ¢ÇÎåvéZ
        Vector3 dist = boss.gameObject.transform.position - player.gameObject.transform.position;
        Vector3 cross = Vector3.Cross(dist.normalized, boss.gameObject.transform.up);
        float angle = Vector3.Dot(dist.normalized, boss.gameObject.transform.up);
        angle = Mathf.Acos(angle) / 3.14f * 180;

        //ÉvÉåÉCÉÑÅ[ÇÃï˚Ç…âÒì]
        if (angle < 177) {
            if (cross.z > 0) {
                boss.transform.Rotate(Vector3.forward * 150 * Time.fixedDeltaTime);
            } else {
                boss.transform.Rotate(Vector3.forward * -150 * Time.fixedDeltaTime);
            }
        } else {
            angle = Mathf.Atan2(dist.y, dist.x);
            angle = angle / 3.14f * 180+90;

            boss.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
   
        
        line.SetPosition(0, boss.gameObject.transform.position);
        line.SetPosition(1, boss.gameObject.transform.position + boss.gameObject.transform.up * 100);

    }
    private void Shot() {
        const float SHOT_RANGE = 45;
        const float LOOP_MAX = 5;

        shotIntervalTimer += Time.fixedDeltaTime;
        shotTimer += Time.fixedDeltaTime;

        if (shotIntervalTimer > SHOT_INTERVAL && shotTimer < SHOT_TIME * 0.5f) {
            shotIntervalTimer = 0;

            //íeê∂ê¨
            for (int i = 0; i < LOOP_MAX; ++i) {
                //É{ÉXÇÃå¸Ç¢ÇƒÇ¢ÇÈäpìxÇãÅÇﬂÇÈ
                float angle = Mathf.Atan2(boss.gameObject.transform.up.y, boss.gameObject.transform.up.x);
                angle = angle / 3.14f * 180;
                boss.line.SetActive(false);
                //äeíeÇÃî≠éÀäpìxÇåvéZ
                float shotAngle = angle - SHOT_RANGE / 2 + (SHOT_RANGE / LOOP_MAX * i);
                //5ïbå„Ç…è¡Ç¶ÇÈíeÇê∂ê¨
                Destroy(Instantiate(boss.bullet, boss.gameObject.transform.position, Quaternion.Euler(0, 0, shotAngle - 90)), 5);
            }
        }

        //ÉåÅ[ÉUÅ[ï`âÊ
        if (shotTimer > SHOT_TIME * 0.5f) {
            boss.line.SetActive(true);
        }

        //àÍíËéûä‘åÇÇ¡ÇΩÇÁÉvÉåÉCÉÑÅ[ÇÃï˚Çå¸Ç≠èàóùÇ…à⁄çs
        if (shotTimer > SHOT_TIME) {
            shotIntervalTimer = 0;
            shotTimer = 0;
            shotCount++;
        }

    }
    private void End() {
        if (shotCount >= 3) {
            boss.SetAction<BossIdleAction>();
        }
    }
}
