using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {
    private List<AudioSource> sounds=new List<AudioSource>();

    public enum SoundIndex {
        BOOST,
        BOUND,
        CHARGE,
        DAMAGE,
        DASH,
        EAT_GOHAN,
        GET_KEY,
        GOHAN,
        HIT_TACKLE,
        MISS,
        PINCHI,
        TACKLE,
        UNLOCK,
        GURUGURU,
        HIT_PIPE,
        HIT_MAT,
        MAX
    }

    private void Start() {
        for (int i = 0; i < transform.GetChildCount(); ++i) {
            sounds.Add(transform.GetChild(i).gameObject.GetComponent<AudioSource>());
        }
    }

    public void PlayShot(SoundIndex i) {
        sounds[(int)i].PlayOneShot(sounds[(int)i].clip);
    }
    public void Play(SoundIndex i) {
        sounds[(int)i].Play();
    }
    public void Stop(SoundIndex i) {
        sounds[(int)i].Stop();
    }
    public void SetPich(SoundIndex i, float p) {
        sounds[(int)i].pitch = p;
    }
}
