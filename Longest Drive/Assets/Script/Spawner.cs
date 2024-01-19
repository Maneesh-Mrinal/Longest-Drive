using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Spawner : MonoBehaviour
{
    
    public GameObject[] obstaclePatterns;
    public GameObject[] ExtraSpawn;

    private PlayerMovement player;

    private float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float minTime = 0;
    public float decreaseTime;
    public float timeLeft = 3.0f;
    public TMP_Text startText;
    public GameObject Timer1;
    public Animator EndAnim;
    int spawnReset = 4;
    int isSpawnedExtra = 0;
    public void Start()
    {
        StartCoroutine(EndScene());
    }
    void Update()
    {
        Debug.Log(isSpawnedExtra);
        timeLeft -= Time.deltaTime;
        startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            GameObject.Destroy(Timer1);
            //Do something useful or Load a new game scene
            if (timeBtwSpawn <= 0)
            {
                int randObs = Random.Range(0, obstaclePatterns.Length);
                int randExtra = Random.Range(0, ExtraSpawn.Length);
                if (spawnReset > 0)
                {
                    Instantiate(obstaclePatterns[randObs], transform.position, Quaternion.identity);
                    spawnReset--;
                }
                else if (spawnReset == 0)
                {
                    isSpawnedExtra = Random.Range(0, 2);
                    if(isSpawnedExtra == 1)
                    {
                        Instantiate(obstaclePatterns[randObs], transform.position, Quaternion.identity);
                    }
                    else if (isSpawnedExtra == 0)
                    {
                        Instantiate(ExtraSpawn[randExtra], transform.position, Quaternion.identity);
                        isSpawnedExtra = 1;
                        spawnReset = 5;
                    }
                }

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
            //Debug.Log(spawnReset);
        }
    }
    private IEnumerator EndScene()
    {
        EndAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
    }
}
