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

    public float GetVolumeBGM() {
        if (PlayerPrefs.GetInt("isBootUp") == 0) {
            return 0.8f;
        } else {
            return PlayerPrefs.GetFloat("BGM")*0.1f;
        }
    }
    public float GetVolumeSE() {
        if (PlayerPrefs.GetInt("isBootUp") == 0) {
            return 0.8f;
        } else {
            return PlayerPrefs.GetFloat("SE")*0.1f;
        }
    }
}
