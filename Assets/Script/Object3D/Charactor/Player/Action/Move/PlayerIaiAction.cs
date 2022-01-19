using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIaiAction : PlayerMoveAction {
    GameObject hitEnemy;

    LineRenderer line;
    GameObject effect;
    Vector3 attackPos;
    Vector3 moveSpeedBuffer;

    public bool isCharge = true;
    bool isEnemyHit = false;
    bool isEnd = false;

    public override void Init(Player p) {
        base.Init(p);


        line = player.line[0].GetComponent<LineRenderer>();
        line.positionCount = 2;

        line.materials[0].color = Color.green;
        line.materials[0].SetColor("_EmissionColor", Color.green);

        player.animator.SetBool("isMove", false);
        player.sound.Play(PlayerSound.SoundIndex.CHARGE);
    }

    public override void CollisionEnter(Collision collision) {
        if (isCharge == false) {
            isEnd = true;
        }
    }

    public override void TriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Enemy")) {
            bool isBattle = false;
            for (int i = 0; i < StageManager.instance.enemyScript.Count; ++i) {
                if (StageManager.instance.enemyScript[i].SCENE_NUM == 3) {
                    isBattle = true;
                }
            }

            if (isBattle==false && collider.gameObject.GetComponent<Enemy>().isSumaki == false) {
                hitEnemy = collider.gameObject;
                isEnemyHit = true;
            }
        }

        if (collider.gameObject.CompareTag("Boss")) {
            //�ړ����x��߂�
            player.moveSpeed *= 0.1f;

            //��������
            player.line[0].SetActive(false);
            Destroy(effect);

            player.animator.SetBool("isMove", true);
            player.animator.SetBool("isIai", false);

            player.ChangeAction<PlayerIaiAction, PlayerMoveAction>();
        }
    }


    public override void Action() {
        Iai();
        End();
    }

    private void Iai() {
        player.transform.position += player.moveSpeed * Time.fixedDeltaTime;

        if (isCharge == true) {
            player.animator.SetBool("isCharge", true);

            effect.transform.position = player.gameObject.transform.position;
            effect.transform.rotation = player.visual.transform.rotation;

            //���̍��W�ݒ�
            line.SetPosition(0, player.transform.position);
            line.SetPosition(1, GetWorldMousePos());
            player.line[0].SetActive(true);

            //�����`���[�W�I������
            if (Input.GetMouseButton(1) == false) {
                isCharge = false;
                attackPos = GetWorldMousePos();
                moveSpeedBuffer = player.moveSpeed;

                player.moveSpeed = (attackPos - player.transform.position).normalized * accelerationBaseSpeed * 5;

                player.animator.SetBool("isCharge", false);
                player.animator.SetBool("isIai", true);

                player.sound.Stop(PlayerSound.SoundIndex.CHARGE);
                player.sound.PlayShot(PlayerSound.SoundIndex.DASH);
            }
        } else {
            //�c��
            GameObject obj = Instantiate(player.afterimage, player.visual.transform.position, player.visual.transform.rotation);
            Destroy(obj, 0.2f);

            //�ڕW�n�_�ɓ��B
            if ((attackPos - player.transform.position).magnitude < 0.5f) {
                isEnd = true;
            }
            effect.SetActive(false);
        }


        //�v���C���[�̌����ڂ���]
        Vector3 dist = player.transform.position - player.positionBuffer;
        Vector3 angle = Vector3.zero;
        angle.z = Mathf.Atan2((dist.y), Mathf.Abs(dist.x)) / 3.14f * 180;
        if (dist.x > 0) {
            angle.y = 0;
        } else {
            angle.y = 180;
        }

        player.visual.transform.rotation = Quaternion.Euler(0.0f, angle.y, angle.z);


    }

    private void End() {
        if (isEnd == true) {
            //�ړ����x��߂�
            player.moveSpeed *= 0.1f;

            //��������
            player.line[0].SetActive(false);
            Destroy(effect);

            player.animator.SetBool("isMove", true);
            player.animator.SetBool("isIai", false);
            if (isEnemyHit == true) {
                //�G�ɓ������Ă�
                player.ChangeAction<PlayerIaiAction, PlayerGuruguruAction>();
                PlayerGuruguruAction guruguruAction = player.GetAction<PlayerGuruguruAction>();
                guruguruAction.SetTartegt(hitEnemy);
            } else {
                //�ړ��ɖ߂�
                player.ChangeAction<PlayerIaiAction, PlayerMoveAction>();

            }
        }

    }

    public void SetEffect(GameObject obj) {
        effect = obj;
    }

}