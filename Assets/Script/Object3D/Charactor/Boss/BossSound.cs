using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour {
    private List<AudioSource> sounds = new List<AudioSource>();

    public enum SoundIndex {
        DAMAGE,
        KILL,
        NO_DAMAGE,
        SHOT,
        MAX
    }

    private void Start() {
        for (int i = 0; i < transform.childCount; ++i) {
            sounds.Add(transform.GetChild(i).gameObject.GetComponent<AudioSource>());
        }
    }

    public void Play(SoundIndex i) {
        sounds[(int)i].PlayOneShot(sounds[(int)i].clip);
    }
    public void Stop(SoundIndex i) {
        sounds[(int)i].Stop();
    }
    public void SetPich(SoundIndex i, float p) {
        sounds[(int)i].pitch = p;
    }
}
