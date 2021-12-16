using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPort : MonoBehaviour {
    [SerializeField] private AudioSource sound;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float accelerationLate;
    [SerializeField] private float STEAM_TIME = 4;
    [SerializeField] private float STEAM_INTERVAL_TIME = 4;

    private float steamTimer = 0;
    private float steamIntervalTimer = 0;

    private bool isSteam = false;


    private void Update() {
        if (isSteam == true) {
            //ö‹C•¬ŽË
            steamTimer += Time.deltaTime;
            if (steamTimer > STEAM_TIME) {
                steamTimer = 0;
                isSteam = false;
            }
        } else {
            //’âŽ~
            particle.Stop();

            steamIntervalTimer += Time.deltaTime;
            if (steamIntervalTimer > STEAM_INTERVAL_TIME) {
                steamIntervalTimer = 0;
                isSteam = true;
                sound.Play();
                particle.Play();
            }
        }


    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (isSteam == false) return;

            Player player = other.gameObject.GetComponent<Player>();

            Vector3 dist = player.transform.position - this.transform.position;
            player.moveSpeed += this.transform.up * dist.magnitude * accelerationLate;

            player.hP.Damage(8, true);
        }
    }
}
