using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour {
    protected bool isEvent;
    protected bool isEventEnd;

    protected void SkipEvent() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isEvent = false;
            isEventEnd = true;
           
        }
    }
    public void SetIsEvent(bool flag) {
        isEvent = flag;
    }
    public bool GetIsEvent() {
        return isEvent;
    }
    public bool GetisEventEnd() {
        return isEventEnd;
    }

}
