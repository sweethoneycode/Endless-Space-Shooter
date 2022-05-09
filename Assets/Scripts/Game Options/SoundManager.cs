using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource, _audioSource;

    public static SoundManager Instance;

    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

    }
    public void PlayStageMusic(AudioClip clip)
    {
        _musicSource.Stop();
        _musicSource.clip = clip;
        _musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {

        _audioSource.clip = clip;
        _audioSource.PlayOneShot(_audioSource.clip);

    }
}
