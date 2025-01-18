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

    public GameState stateGamePlay = GameState.Intro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (stateGamePlay == GameState.Intro)
        {

            StartCoroutine(ActivateAfterDelay(3f));
        }
        else if (stateGamePlay == GameState.Play)
        {
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
        //show the target food in idea
        yield return new WaitForSeconds(delay);
        stateGamePlay = GameState.Play;
    }

    public void SetEndGame()
    {
        stateGamePlay = GameState.GameOver;
    }
}
