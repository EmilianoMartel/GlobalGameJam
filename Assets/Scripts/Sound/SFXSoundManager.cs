using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXSoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _type = "Master";
    [SerializeField] private float _waitForSet = 0.5f;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private SoundSFCSourcer _soundSFCSourcer;

    private void OnEnable()
    {
        _soundSFCSourcer.SoundManager = this;
        if (PlayerPrefs.HasKey(_type))
            StartCoroutine(Volume());
    }

    public void PlayAudio(AudioClip audio)
    {
        _audio.clip = audio;
        _audio.Play();
    }

    private IEnumerator Volume()
    {
        yield return new WaitForSeconds(_waitForSet);
        LoadVolume();
    }

    private void LoadVolume()
    {
#if UNITY_WEBGL
#else
        float volumen = PlayerPrefs.GetFloat(_type);
        _mixer.SetFloat(_type, Mathf.Log10(volumen) * 20);
        volumen = PlayerPrefs.GetFloat("Music");
        _mixer.SetFloat("Music", Mathf.Log10(volumen) * 20);
        volumen = PlayerPrefs.GetFloat("SFX");
        _mixer.SetFloat("SFX", Mathf.Log10(volumen) * 20);
#endif
    }
}