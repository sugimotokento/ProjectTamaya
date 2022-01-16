using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemAnimation : MonoBehaviour {
    GameObject target;

    float timer = 0;
    // Start is called before the first frame update
    void Start() {
        Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if (target == null) return;
        Vector3 position= target.transform.position;
        position.y += Mathf.Sin(timer*10)*0.2f+1.5f;
        this.transform.position = position;
        
    }

    public void Init(GameObject obj, int itemIndex) {
        target = obj;

        //設定したアイテムを表示する
        for(int i = 0; i < transform.childCount; ++i) {
            if (i == itemIndex) {
                transform.GetChild(i).gameObject.SetActive(true);
            } else {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
