using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI score;
    public TMPro.TextMeshProUGUI bestscore;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject firstRulePanel;
    [SerializeField] private UnityEngine.UI.Button retryButton;
    [SerializeField] private UnityEngine.UI.Button quitButton;
    [SerializeField] private UnityEngine.UI.Button rulesButton;

    private GameManager _gm;

    private void Awake()
    {
        _gm = GameManager.Instance;
        gameOverPanel.SetActive(false);
        retryButton.onClick.AddListener(OnRetryClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
    }
    
    private void Start()
    {
        // Désactivez la première page de règles au début
        startPanel.SetActive(false);

        // Ajouter un listener au bouton "Rules"
        rulesButton.onClick.AddListener(OpenFirstRulePanel);
    }

    private void Update()
    {
        score.text = $"SCORE: {_gm.ScoreManager.Score}";
        bestscore.text = $"BEST SCORE: {_gm.ScoreManager.BestScore}";
    }
    
    public void OpenFirstRulePanel()
    {
        firstRulePanel.SetActive(true);
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
    
    public void ShowStart()
    {
        startPanel.SetActive(true);
    }
    
    public void HideGameOver()
    {
        gameOverPanel.SetActive(false);
    }
    
    private void OnRetryClicked()
    {
        HideGameOver();
        // Relance la partie : on peut passer par GameManager
        GameManager.Instance.StartGame();
    }

    private void OnQuitClicked()
    {
        HideGameOver();
        ShowStart();
    }
}
