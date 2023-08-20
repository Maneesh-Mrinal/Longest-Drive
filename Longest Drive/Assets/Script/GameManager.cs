using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator StartAnim;
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
