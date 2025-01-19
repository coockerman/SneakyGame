using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] AudioClip stepFoot;
    [SerializeField] AudioClip cooking;
    [SerializeField] private AudioClip stepCrack;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource fxSource;

    public void OnStepFoot()
    {
        fxSource.volume = 1f;
        fxSource.PlayOneShot(stepFoot);
    }

    public void OnStepFootCrack()
    {
        fxSource.volume = 1f;
        fxSource.PlayOneShot(stepCrack);
    }
    public void OnCookingSFX()
    {
        fxSource.volume = 0.2f;
        fxSource.PlayOneShot(cooking);
    }
    
    
}
