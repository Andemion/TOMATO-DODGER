using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public TomatoManager TomatoManager {get; private set;}
    public ScoreManager ScoreManager {get; private set;}
    public UIManager UIManager {get; private set;}
    //public AudioManager AudioManager {get; private set;}

    private void Awake()
    {
        // singleton
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        TomatoManager = GetComponent<TomatoManager>();
        ScoreManager = GetComponent<ScoreManager>();
        UIManager = GetComponent<UIManager>();
        //AudioManager = GetComponent<AudioManager>();
    }

    private void TimeUpHandler(){
        StopGame();
    }

    public void StartGame()
    {
        UIManager.StartGame();
        TomatoManager.StartSpawning();
        ScoreManager.Reset();
        //AudioManager.StartGame();
    }

    public void StopGame()
    {
        TomatoManager.StopSpawning();
        UIManager.StopGame();
    }
}
