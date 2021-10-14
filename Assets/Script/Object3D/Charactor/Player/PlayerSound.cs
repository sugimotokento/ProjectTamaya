using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> sound;

    public enum SoundIndex {
        MOVE,
        MAX
    }


    public void PlayShot(SoundIndex i) {
        audioSource.PlayOneShot(sound[(int)i]);
    }
    public void SetPich(float p) {
        audioSource.pitch = p;
    }
}
