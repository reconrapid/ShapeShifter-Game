using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAudioManager : MonoBehaviour
{

    private AudioManager audioManager;

    void Start()
    {
        if (GameObject.FindWithTag("AudioManager") != null)
        {
            audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        }
    }

    public void SelectTrack(AudioClip clip)
    {
        audioManager.PlayTrack(clip);
    }

    public void SelectEffect(AudioClip clip)
    {
        audioManager.PlayEffect(clip);
    }

}
