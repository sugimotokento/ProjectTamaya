using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
    private CameraAction action;

    private void Awake() {
        SetAction<PlayerCameraAction>();
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }

    private void FixedUpdate() {
        action.Action();
    }


    public void SetAction<T>() where T : CameraAction, new() {
        action = new T();
        action.Init(this);
    }
    public T GetAction<T>() where T : CameraAction {
        return (T)(object)action;
    }
    public bool CheckAction<T>() where T : CameraAction {
        if (typeof(T) == action.GetType()) {
            return true;
        }

        return false;
    }

}
