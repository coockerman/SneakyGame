using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Intro, Play, Pause, GameOver, GamePass
    }
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public SOFood targetFood;
    [SerializeField] int currentLevel = 1;
    public int CurrentLevel { get { return currentLevel; } }
    public GameObject ideaFood;
    public GameState stateGamePlay = GameState.Intro;
    public float maxTimeMap = 120f;
    public float countTimeMap = 0;
    bool isEndGame = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countTimeMap = maxTimeMap;
        StartCoroutine(ActivateAfterDelay(3f));
    }

    // Update is called once per frame
    void Update()
    {
        if (stateGamePlay == GameState.Intro)
        {
            
        }
        else if (stateGamePlay == GameState.Play)
        {
            countTimeMap -= Time.deltaTime;
            UIManager.Instance.UpdateTime((int)countTimeMap);
            if(countTimeMap <= 0)
            {
                SetEndGame();
            }
            // Logic Play state
        }
        else if (stateGamePlay == GameState.Pause)
        {
            // Logic Pause state
        }
        else if (stateGamePlay == GameState.GameOver)
        {
            // Logic GameOver state
        }
        else if (stateGamePlay == GameState.GamePass)
        {
            // Logic GamePass state
        }
    }

    private IEnumerator ActivateAfterDelay(float delay)
    {
        ideaFood.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        ideaFood.gameObject.SetActive(false);
        stateGamePlay = GameState.Play;
    }

    public void SetEndGame()
    {
        if (!isEndGame)
        {
            isEndGame = true;
            stateGamePlay = GameState.GameOver;
            UIManager.Instance.OnLoseUI();
        }
        
    }

    public void SetWinGame()
    {
        if (!isEndGame)
        {
            isEndGame = true;
            stateGamePlay = GameState.GamePass;
            UIManager.Instance.OnWinningUI();
        }
        
    }
    
    void StopStatusGame()
    {
        
    }
}
