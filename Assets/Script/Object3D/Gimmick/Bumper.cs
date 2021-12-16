using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

    [SerializeField] private GameObject visual;
    [SerializeField] private AudioSource sound;
    [SerializeField] private Animator animator;
    [SerializeField] private float reflectionPower;
    private bool isAnimation = false;
    private int frameCount = 0;

    private const float ANIMATION_TIME = 0.5f;

    private void Update() {
        if (isAnimation == true) {
            frameCount++;
            if (frameCount > 1) {
                animator.SetBool("isReflection", false);
                frameCount = 0;
                isAnimation = false;
            }
           
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag=="Player") {
            Player player = collision.gameObject.GetComponent<Player>();
            Vector3 dist = collision.gameObject.transform.position - this.transform.position;
            dist.z = 0;
            float moveSpeed = player.moveSpeed.magnitude;

            //”½ŽË
            player.moveSpeed = dist.normalized * moveSpeed * reflectionPower;
            isAnimation = true;

            animator.SetBool("isReflection", true);
            sound.Play();
        }
    }
}
