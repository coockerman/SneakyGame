using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState
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

    private GameState stateGamePlay = GameState.Intro; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stateGamePlay == GameState.Intro)
        {

        }else if (stateGamePlay == GameState.Play)
        {

        }
    }
    public void SetEndGame()
    {
        stateGamePlay = GameState.GameOver;
    }
}
