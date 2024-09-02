using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayClip(AudioClip clip, Transform spawnTransform)
    {
        AudioSource source = Instantiate(audioSource, spawnTransform.position, Quaternion.identity);
        source.clip = clip;
        source.volume = .2f;
        
        source.Play();

        float length = source.clip.length;
        
        Destroy(source.gameObject, length);
    }
}
