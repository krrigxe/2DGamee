using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource introSource, loopSourse;
    // Start is called before the first frame update
    void Start()
    {
        introSource.Play();
        loopSourse.PlayScheduled(AudioSettings.dspTime + introSource.clip.length);
    }
}
