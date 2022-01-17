using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    private List<AudioSource> EnemySounds = new List<AudioSource>();

    public enum EnemySoundIndex
    {
        PopBallet,
        Contact,
        MAX
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            EnemySounds.Add(transform.GetChild(i).gameObject.GetComponent<AudioSource>());
        }
    }

    public void EnemyPlay(EnemySoundIndex i)
    {
        EnemySounds[(int)i].PlayOneShot(EnemySounds[(int)i].clip);
    }
    public void EnemyStop(EnemySoundIndex i)
    {
        EnemySounds[(int)i].Stop();
    }
    public void EnemySetPich(EnemySoundIndex i, float p)
    {
        EnemySounds[(int)i].pitch = p;
    }
}
