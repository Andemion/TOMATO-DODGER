using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI score;
    public TMPro.TextMeshProUGUI bestscore;
    //public TMPro.TextMeshProUGUI timer;
    public GameObject startButton;

    private GameManager _gm;

    private void Awake()
    {
        _gm = GameManager.Instance;
    }

    private void Update()
    {
        score.text = $"SCORE: {_gm.ScoreManager.Score}";
        bestscore.text = $"BEST SCORE: {_gm.ScoreManager.BestScore}";
        // timer.text = $"TIME {TimeSpan.FromSeconds(_gm.TimeManager.Remaining):mm\\:ss}";
    }

    public void StartGame()
    {
        startButton.SetActive(false);
    }

    public void StopGame()
    {
        startButton.SetActive(true);
    }
}
