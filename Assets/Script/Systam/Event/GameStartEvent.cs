using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartEvent : GameEvent
{
    // Start is called before the first frame update
    void Start()
    {
        isEvent = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canEvent == true) {
            StageManager.instance.isGameStart = true;
            isEvent = false;
            canEvent = false;
        }
    }
}
