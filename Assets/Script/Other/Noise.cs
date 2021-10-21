using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour {
    private SphereCollider collider;

    private void Awake() {
        collider = GetComponent<SphereCollider>();
    }


    public void SetDestroy(float timer = 0.1f) {
        Destroy(this.gameObject, timer);
    }
    public void SetRadius(float radius) {
        collider.radius = radius;
    }
}
