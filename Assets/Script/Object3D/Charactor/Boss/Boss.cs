using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    private BossAction action;

    public GameObject bullet;
    public GameObject line;

    // Start is called before the first frame update
    void Start() {
        SetAction<BossIdleAction>();
    }

    // Update is called once per frame
    void Update() {

    }
    
    private void FixedUpdate() {
        action.Action();
    }


    public T GetAction<T>() where T : BossAction {
        return (T)(object)action;
    }
    public bool CheckAction<T>() where T : BossAction {
        if (typeof(T) == action.GetType()) {
            return true;
        } else {
            return false;
        }
    }

    public void SetAction<T>() where T : BossAction, new() {
        action = new T();
        action.Init(this);
    }
}

