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
        float beginPlaying = Time.time + delayOnStart;

        if (playOnStart && Time.time > beginPlaying)
        {
            ShuffleMusicTracks();
        }
    }

    private void ShuffleMusicTracks()
    {
        trackList.Shuffle();

        foreach (var track in trackList)
        {
            StartCoroutine(PlayTrack(track));
        }
    }

    private IEnumerator PlayTrack(AudioClip track)
    {
        SoundManager.PlaySound(track, volume);

        yield return new WaitForSecondsRealtime(delayBetweenTracks);
    }
}
