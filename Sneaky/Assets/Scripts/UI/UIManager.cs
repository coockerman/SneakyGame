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
    [SerializeField] GameObject bubble;
    [SerializeField] Slider sliderDad;
    [SerializeField] Slider sliderDog;


    [SerializeField] float maxScale = 8f; 
    [SerializeField] float minScale = 4f;

    public void Start()
    {
        // Kiểm tra nếu bubble chưa được gán, tìm đối tượng có tên "Bubble" (hoặc tên của đối tượng bubble trong scene)
        if (bubble == null)
        {
            bubble = GameObject.Find("bubble_0");
            if (bubble == null)
            {
                Debug.LogError("Bubble not found. Please assign the bubble GameObject.");
            }
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
    public void UpdateBubbleSize(float sizeFactor)
    {
        if (bubble != null)
        {
            // Tính toán tỷ lệ mới cho bubble, từ minScale đến maxScale
            float newScale = Mathf.Lerp(minScale, maxScale, sizeFactor);
            bubble.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
        
    }
}

