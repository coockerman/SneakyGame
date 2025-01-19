using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject winningUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] Slider sliderDad;
    [SerializeField] Slider sliderDog;

    [SerializeField] float maxScale = 8f; 
    [SerializeField] float minScale = 4f;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject clickPrefab;
    
    public void OnPlayerNear(Vector3 position)
    {
        GameObject click = Instantiate(clickPrefab, position, Quaternion.identity);
        Destroy(click, 1f);
    }
    public void OnUISetting()
    {
        settingUI.SetActive(true);
    }
    public void OffnUISetting()
    {
        settingUI.SetActive(false);
    }
    public void UpdateTime(int time)
    {
        int minutes = Mathf.FloorToInt(time / 60); // Tính số phút
        int seconds = Mathf.FloorToInt(time % 60); // Tính số giây còn lại
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void OnWinningUI()
    {
        winningUI.SetActive(true);
    }
    public void OffWinningUI()
    {
        winningUI.SetActive(false);
    }
    public void OnLoseUI()
    {
        loseUI.SetActive(true);
    }
    public void OffLoseUI()
    {
        loseUI.SetActive(false);
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
        SceneManager.LoadScene(0);
    }

    public void NextMap()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextScene >= SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

