using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    static public SoundManager instance=null;
    [SerializeField] PauseManager pause;


    private void Awake() {
        if(instance==null) {
            instance = this;
        }
    }

    public float GetVolumeBGM() { return pause.GetSlider(0).value*0.1f; }
    public float GetVolumeSE() { return pause.GetSlider(1).value*0.1f; }
}
