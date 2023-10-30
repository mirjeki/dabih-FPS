using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] bool playOnStart;
    [SerializeField] float delayOnStart = 2f;
    [SerializeField] float delayBetweenTracks = 5f;
    [SerializeField] float volume = 0.5f;

    [Header("Music")]
    [SerializeField] List<AudioClip> trackList = new List<AudioClip>();

    void Start()
    {
        if (playOnStart)
        {
            Invoke("PlayTracklist", delayOnStart);
        }
    }

    void PlayTracklist()
    {
        StartCoroutine(ShuffleMusicTracks());
    }

    private IEnumerator ShuffleMusicTracks()
    {
        trackList.Shuffle();

        float beginPlaying = Time.time + delayOnStart;

        foreach (var track in trackList)
        {
            SoundManager.PlaySound(track, volume);

            yield return new WaitForSeconds(delayBetweenTracks + track.length);
        }
    }
}
