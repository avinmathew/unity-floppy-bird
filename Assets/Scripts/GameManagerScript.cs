using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    public GameObject titleImage;
    public GameObject gameOverImage;
    public GameObject playButton;

    public AudioSource audioSource;
    public AudioClip startGameSound;

    public BirdScript birdScript;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        
        // Don't allow user to control bird before play button is started
        birdScript.enabled = false;
        //GameState.BestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    private void Start()
    {
        // When we restart, best score is set to zero, so check what actual best score is
        bestScoreText.text = "Best:" + Environment.NewLine + GameState.BestScore.ToString();
    }

    public void AddScore(int score)
    {
        GameState.AddScore(score);
        scoreText.text = "Score:" + Environment.NewLine + GameState.Score.ToString();
    }

    public void Play()
    {
        GameState.IsGameStart = true;
        GameState.Score = 0;
        scoreText.text = "Score:" + Environment.NewLine + GameState.Score.ToString();

        titleImage.SetActive(false);
        gameOverImage.SetActive(false);
        playButton.SetActive(false);

        PipeScript[] pipes = FindObjectsOfType<PipeScript>();
        foreach (PipeScript pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }

        birdScript.enabled = true;
        Time.timeScale = 1;

        audioSource.clip = startGameSound;
        audioSource.Play();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        birdScript.enabled = false;
    }

    public void GameOver()
    {
        // Activate game over screen
        gameOverImage.SetActive(true);
        playButton.SetActive(true);

        GameState.UpdateBestScore();
        bestScoreText.text = "Best:" + Environment.NewLine + GameState.BestScore.ToString();

        Pause();
    }
}
