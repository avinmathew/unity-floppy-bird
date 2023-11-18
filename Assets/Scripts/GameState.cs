using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class GameState
{
    public static int Score = 0;
    public static int BestScore = 0;
    public static bool IsBirdDead = false;
    public static bool IsGameStart = false;

    public static void AddScore(int score)
    {
        Score += score;
    }

    public static void UpdateBestScore()
    {
        // Save best score
        if (Score > BestScore)
        {
            BestScore = Score;
            //PlayerPrefs.SetInt("BestScore", BestScore);
        }
    }
}
