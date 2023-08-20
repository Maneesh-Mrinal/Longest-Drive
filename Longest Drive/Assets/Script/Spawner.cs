using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Spawner : MonoBehaviour
{
    
    public GameObject[] obstaclePatterns;

    private float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float minTime = 0;
    public float decreaseTime;
    public float timeLeft = 3.0f;
    public TMP_Text startText;
    public GameObject Timer1;
    public Animator EndAnim;
    public void Start()
    {
        StartCoroutine(EndScene());
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            GameObject.Destroy(Timer1);
            //Do something useful or Load a new game scene
            if (timeBtwSpawn <= 0)
            {
                int rand = Random.Range(0, obstaclePatterns.Length);
                Instantiate(obstaclePatterns[rand], transform.position, Quaternion.identity);
                timeBtwSpawn = startTimeBtwSpawn;

                if (startTimeBtwSpawn > minTime)
                {

                    startTimeBtwSpawn -= decreaseTime;
                }
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }
    }
    private IEnumerator EndScene()
    {
        EndAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
    }
}
