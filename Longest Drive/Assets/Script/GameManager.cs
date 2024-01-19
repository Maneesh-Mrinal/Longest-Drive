using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public Animator StartAnim;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI currentscoreText;
    public int currentScore;
    public int highScore;

    void Start()
    {
        LoadPlayer();
    }
    void Update()
    {
        highscoreText.text = highScore.ToString();
        currentscoreText.text = currentScore.ToString();
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        highScore = data.highScore;
        currentScore = (int)data.currentScore;
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
