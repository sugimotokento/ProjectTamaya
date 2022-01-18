using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSoundVolume : MonoBehaviour {
    [SerializeField] private bool isBGM = false;
    [SerializeField] private bool isUI = false;
    private AudioSource audioSouce;
    [SerializeField] private float baseVolume = 1;
    [SerializeField] private float volumeRange = 15;
    // Start is called before the first frame update
    void Start() {
        audioSouce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (isBGM == true) {
            audioSouce.volume = baseVolume * SoundManager.instance.GetVolumeBGM();
        } else {
            float distVolume = 1;
            if (isUI == false) {
                Vector3 dist = this.transform.position - StageManager.instance.player.gameObject.transform.position;
                dist.z = 0;
                distVolume = volumeRange - dist.magnitude;
                distVolume /= volumeRange;
                if (distVolume < 0) distVolume = 0;
            }

            audioSouce.volume = baseVolume * SoundManager.instance.GetVolumeSE()*distVolume;

        }
    }
}
