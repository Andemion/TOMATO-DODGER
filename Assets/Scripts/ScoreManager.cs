using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public int Score {get; private set;} = 0;
    public int BestScore {get; private set;} = 0;
    private GameManager _gm;

    private void Awake()
    {
        _gm = GameManager.Instance;
        _gm.TomatoManager.OnCollected += TomatoCollectedHandler;
    }

    private void Start()
    {
        BestScore = PlayerPrefs.GetInt("best_score", 0);
    }

    private void TomatoCollectedHandler(Tomato tomato)
    {
        Score++;
        if(Score > BestScore)
        {
            BestScore = Score;
            PlayerPrefs.SetInt("best_score", BestScore);
        }
    }

    public void Reset()
    {
        Score = 0;
    }

}
