using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public static void PlaySound(AudioClip clip, Vector3 position)
    {
        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }

    public static void PlaySound(AudioClip clip, float volume)
    {
        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.PlayOneShot(clip);

        Object.Destroy(soundObject, clip.length);
    }

    public static void PlaySound(AudioClip clip)
    {
        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(clip);

        Object.Destroy(soundObject, clip.length);
    }
}
