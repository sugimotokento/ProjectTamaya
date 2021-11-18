using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour {
    protected Boss boss;

    public virtual void Init(Boss b) { boss = b; }
    public virtual void Action() { }
}
