using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject settingUI;
    [SerializeField] Slider sliderDad;
    [SerializeField] Slider sliderDog;
    [SerializeField] SoundManager soundManager;


    void Start()
    {
        // Kiểm tra nếu Slider đã được gán
        if (sliderDad != null && soundManager != null)
        {
            sliderDad.value = AudioListener.volume;
            sliderDad.onValueChanged.AddListener(OnMusicVolumeChanged);
        }
    }
    public void OnUISetting()
    {
        settingUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void OffnUISetting()
    {
        settingUI.SetActive(false);
        Time.timeScale = 1;
    }  
    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeMusic()
    {
        soundManager.ToggleMute();
    }

    public void OnMusicVolumeChanged(float volume)
    {
        soundManager.SetMusicVolume(volume); // Cập nhật âm lượng từ UI
    }

    public void BackToHome()
    {
       
    }

    public void NextMap()
    {

    }

    public void Replay()
    {

    }

    public void UpdateSliderDad(float count)
    {
        sliderDad.value = count;
    }
    public void UpdateSliderDog(float count)
    {
        sliderDog.value = count;
    }


}

