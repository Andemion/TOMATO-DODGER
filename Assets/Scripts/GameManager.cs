using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public TomatoManager TomatoManager {get; private set;}
    public ScoreManager ScoreManager {get; private set;}
    public UIManager UIManager {get; private set;}
    public RulesNavigation RulesNavigation {get; private set;}
    public AudioManager AudioManager {get; private set;}
    
    [SerializeField] private Player player;

    private void Awake()
    {
        // singleton
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        TomatoManager = GetComponent<TomatoManager>();
        ScoreManager = GetComponent<ScoreManager>();
        UIManager = FindFirstObjectByType<UIManager>();
        //AudioManager = GetComponent<AudioManager>();
        
        if (player != null)
            player.OnDeath += HandlePlayerDeath;
    }
    

    private void HandlePlayerDeath()
    {
        StopGame();
    }

    public void StartGame()
    {
        TomatoManager.StartSpawning();
        ScoreManager.Reset();
        UIManager.HideGameOver();
        player.ResetLife();
        //AudioManager.StartGame();
    }

    public void StopGame()
    {
        TomatoManager.StopSpawning();
        UIManager.ShowGameOver();
    }
}
