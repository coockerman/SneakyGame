using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip stepFoot;
    [SerializeField] AudioClip stepFoot2;
    public AudioListener audioListener;
    public Slider volumeSlider;
    bool isMusicMuted = false;
    void Start()
    {
        // Kiểm tra Slider có được gán không
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        AudioListener.volume = volume;
        if (volume > 0)
        {
            isMusicMuted = false;
        }// Cập nhật âm lượng
    }
    public void ToggleMute()
    {
        isMusicMuted = !isMusicMuted;
        AudioListener.volume = isMusicMuted ? 0 : 1;
    }
}
