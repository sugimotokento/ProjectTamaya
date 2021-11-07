using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour {
    protected bool isEvent;
    protected bool canEvent;

    private int frameCount = 0;

    protected void SkipEvent() {
        frameCount++;
        if (Input.GetKeyDown(KeyCode.Escape) && frameCount>2) {
            isEvent = false;
           
        }
    }
    public void SetIsEvent(bool flag) {
        isEvent = flag;
    }
    public bool GetIsEvent() {
        return isEvent;
    }
    public void SetCanEvent(bool flag) {
        canEvent = flag;
    }
    public bool GetCanEvent() {
        return canEvent;
    }
}
