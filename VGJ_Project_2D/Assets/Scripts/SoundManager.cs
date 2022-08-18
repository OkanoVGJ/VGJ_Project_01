using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource BGMSource;

    [SerializeField]
    private AudioSource[] SESource;

    public void PlaySE(AudioClip SEClip)
    {
        foreach(var source in SESource)
        {
            if(source.isPlaying)
            {
                source.clip = SEClip;
                source.loop = false;
                source.Play();
                break;
            }
        }
    }

    public void PlayBGM(AudioClip BGMClip)
    {
        BGMSource.Stop();
        BGMSource.loop = true;
        BGMSource.clip = BGMClip;
        BGMSource.Play();
    }
}
