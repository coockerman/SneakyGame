using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject settingUI;
    [SerializeField] Slider sliderDad;
    [SerializeField] Slider sliderDog;
    [SerializeField] SoundManager soundManager;

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

