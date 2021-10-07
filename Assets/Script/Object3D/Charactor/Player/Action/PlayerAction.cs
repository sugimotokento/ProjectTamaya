using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
    protected Player player;
    virtual public void Init(Player p) { player = p; }

    virtual public void UpdateAction() { }
    virtual public void Action() { }
}
