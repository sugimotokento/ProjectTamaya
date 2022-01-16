using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBodyCollider : MonoBehaviour
{
    [SerializeField] Boss boss;
    private Player player;


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (player == null) player = other.gameObject.GetComponent<Player>();


            if (player.CheckAction<PlayerIaiAction>() == true) {
                if (player.GetAction<PlayerIaiAction>().isCharge == false) {
                    //�_���[�W
                    boss.sound.Play(BossSound.SoundIndex.NO_DAMAGE);
                }
            }

            if (player.CheckAction<PlayerTackleAction>() == true) {
                //�^�b�N��
                boss.sound.Play(BossSound.SoundIndex.NO_DAMAGE);
            }



            Vector3 dist = other.gameObject.transform.position - this.transform.position;
            dist.z = 0;
            float moveSpeed = player.moveSpeed.magnitude;

            //����
            player.moveSpeed = dist.normalized * 8;
        }

    }

}
