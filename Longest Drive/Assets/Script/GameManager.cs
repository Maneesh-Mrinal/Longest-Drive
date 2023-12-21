using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.Core;
using System.Threading.Tasks;*/   
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
