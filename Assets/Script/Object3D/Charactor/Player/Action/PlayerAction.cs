using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
    protected Player player;
    virtual public void Init(Player p) { player = p; }

    virtual public void UpdateAction() { }
    virtual public void Action() { }
    virtual public void CollisionEnter(Collision collision) { }
    virtual public void TriggerEnter(Collider collider) {  }

    protected Vector3 GetWorldMousePos() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        return mousePos;
    }
}
