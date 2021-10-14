using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour {
    [SerializeField]
    private float timer;

    // Start is called before the first frame update
    void Start() {
        Destroy(this.gameObject, timer);
    }

    // Update is called once per frame
    void Update() {

    }
}
