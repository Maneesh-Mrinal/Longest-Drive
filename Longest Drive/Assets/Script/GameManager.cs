using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Animator StartAnim;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI currentscoreText;
    int CurrentScore;
    int HiScore;

    void Start()
    {
        CheckHighScore();
    }
    public void CheckHighScore()
    {
        CurrentScore = PlayerPrefs.GetInt("CurrentScore");
        if(CurrentScore > PlayerPrefs.GetInt("HighScore", CurrentScore))
        {
            PlayerPrefs.SetInt("HighScore", CurrentScore);
        }
        UpdateHighScoreText();
    }
    public void UpdateHighScoreText()
    {
        currentscoreText.text = CurrentScore.ToString();
        highscoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void PlayButton()
    {
        StartCoroutine(LoadScenePlay());
    }
    public void RetryButton()
    {
        SceneManager.LoadSceneAsync("MainGame");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    private IEnumerator LoadScenePlay()
    {
        StartAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
