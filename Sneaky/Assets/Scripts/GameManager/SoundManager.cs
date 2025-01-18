using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip stepFoot;
    [SerializeField] AudioClip stepFoot2;
    bool isMusicMuted = false;

    public void SetMusicVolume(float volume)
    {
        AudioListener.volume = volume;
        if (volume > 0)
        {
            isMusicMuted = false;
        }
    }
    public void ToggleMute()
    {
        isMusicMuted = !isMusicMuted;
        AudioListener.volume = isMusicMuted ? 0 : 1;
    }
}
