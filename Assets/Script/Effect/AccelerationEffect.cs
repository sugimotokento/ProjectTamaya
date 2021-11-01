using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationEffect : MonoBehaviour {
    private Renderer renderer;
    float timer = 0;
    // Start is called before the first frame update
    void Start() {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {
        renderer.material.SetFloat("_Timer", timer);
        timer += Time.deltaTime;
    }
}
