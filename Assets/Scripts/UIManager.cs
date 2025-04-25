using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI score;
    public TMPro.TextMeshProUGUI bestscore;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject rulesPanels;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject firstRulePanel;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button rulesButton;
    private RulesNavigation RulesNavigation;

    private GameManager _gm;

    private void Awake()
    {
        _gm = GameManager.Instance;
        gameOverPanel.SetActive(false);
        gameUI.SetActive(false);
        rulesPanels.SetActive(false);
        retryButton.onClick.AddListener(OnRetryClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
        RulesNavigation = GetComponent<RulesNavigation>();
    }
    
    private void Start()
    {
        // Désactivez la première page de règles au début
        startPanel.SetActive(true);

        // Ajouter un listener au bouton "Start" et "Rules"
        startButton.onClick.AddListener(StartGame);
        rulesButton.onClick.AddListener(OpenFirstRulePanel);
    }

    private void Update()
    {
        score.text = $"SCORE: {_gm.ScoreManager.Score}";
        bestscore.text = $"BEST SCORE: {_gm.ScoreManager.BestScore}";
    }
    
    public void OpenFirstRulePanel()
    {
        startPanel.SetActive(false);
        rulesPanels.SetActive(true);
        RulesNavigation.OpenRulePanel();
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        gameUI.SetActive(true);
        GameManager.Instance.StartGame();
    }

    public void ShowGameOver()
    {
        gameUI.SetActive(false);
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
        GameManager.Instance.StartGame();
    }

    private void OnQuitClicked()
    {
        HideGameOver();
        ShowStart();
    }
}
