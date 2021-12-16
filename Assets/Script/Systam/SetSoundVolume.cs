using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSoundVolume : MonoBehaviour {
    [SerializeField] private bool isBGM = false;
    private AudioSource audioSouce;
    [SerializeField] private float baseVolume = 1;
    // Start is called before the first frame update
    void Start() {
        audioSouce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (isBGM == true) {
            audioSouce.volume = baseVolume * SoundManager.instance.GetVolumeBGM();
        } else {
            audioSouce.volume = baseVolume * SoundManager.instance.GetVolumeSE();
        }
    }
}
