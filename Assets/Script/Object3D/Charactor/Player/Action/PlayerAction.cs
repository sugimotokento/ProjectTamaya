using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
    protected Player player;
    virtual public void Init(Player p) { player = p; }

    virtual public void UpdateAction() { }
    virtual public void Action() { }
    virtual public void CollisionEnter() { }

    protected Vector3 GetWorldMousePos() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return mousePos;
    }
}
