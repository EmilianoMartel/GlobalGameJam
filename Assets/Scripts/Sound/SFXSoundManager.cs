using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private SoundSFCSourcer _soundSFCSourcer;

    private void OnEnable()
    {
        _soundSFCSourcer.SoundManager = this;
    }

    public void PlayAudio(AudioClip audio)
    {
        _audio.clip = audio;
        _audio.Play();
    }
}