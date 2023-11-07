using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    private static SoundAssets s_Instance;

    public static SoundAssets instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = Instantiate(Resources.Load<SoundAssets>("SoundAssets"));
            }
            return s_Instance;
        }
    }

    [Header("Player Audio")]
    public AudioClip rifleShot;
    public AudioClip shotgunShot;
    public AudioClip footstepGravel;
    public AudioClip footstepPlating;
    public AudioClip footstepDefault;
    public AudioClip jump;
    public AudioClip hurt;

    [Header("Pickups Audio")]
    public AudioClip ammo;
    public AudioClip medkit;

    [Header("Environment")]
    public AudioClip computer;
    public AudioClip generator;
    public AudioClip button;

    [Header("Enemy Audio")]
    public AudioClip dogBite;
    public AudioClip dogHurt;
    public AudioClip dogWalk;
    public AudioClip alienKick;
    public AudioClip alienShot;
    public AudioClip alienHurt;
    public AudioClip alienWalk;
}
