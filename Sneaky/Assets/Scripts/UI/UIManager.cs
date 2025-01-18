using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] GameObject settingUI;
    [SerializeField] Slider sliderDad;
    [SerializeField] Slider sliderDog;
    
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
        
    }

    public void ChangeFSX()
    {

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

