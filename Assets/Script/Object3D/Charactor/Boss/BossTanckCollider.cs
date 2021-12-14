using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTanckCollider : MonoBehaviour {
    [SerializeField] Boss boss;
    private Player player;
   

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (player == null) player = other.gameObject.GetComponent<Player>();


            if (player.CheckAction<PlayerIaiAction>() == true) {
                if (player.GetAction<PlayerIaiAction>().isCharge == false) {
                    //ダメージ
                    boss.Damage();
                }
            }

            if (player.CheckAction<PlayerTackleAction>() == true) {
                //タックル
                boss.Damage();
            }
        }

    }

}
