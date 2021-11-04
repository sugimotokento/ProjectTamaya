using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour {
    [SerializeField] private Text keyText;

    private int key = 100;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        keyText.text = "✖" + key.ToString();
    }

    public int GetKey() {
        return key;
    }
    public void AddKey(int add = 1) {
        key += add;
    }
    public void UseKey(int num = 1) {
        key -= num;
    }
}
