using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePipe : MonoBehaviour {


    private void OnTriggerStay(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Player player=collision.gameObject.GetComponent<Player>();
            player.hP.Damage((int)((float)player.hP.GetMaxHp() * 0.1f));
        }
    }

}
