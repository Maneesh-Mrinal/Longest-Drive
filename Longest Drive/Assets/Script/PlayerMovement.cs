using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.Core;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    private Vector2 movement;
    private Rigidbody2D rb;
    public TMP_Text HealthText;
    public TMP_Text ScoreText;
    public int health = 3;
    public Color flashColor;
    public Color flashColorPower;
    public Color regularColor;
    public float flashDuration;
    public float flashDurationPower;
    public int numberOfFlashes;
    public int numberOfFlashesPower;
    public Collider2D triggerCollider;
    public SpriteRenderer playerSprite;
    public float score = 0;
    public Animator ScorePop;
    public int HighScore;
    public float timeLeft = 3.0f;
    public FloatingJoystick js;
    public bool isHit = false;
    public bool isPowered = false;
    public GameObject SpawnerObject;
    
    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();        
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerPrefs.SetInt("CurrentScore", 0);
    }

    async void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            HealthText.text = health.ToString();

            float InputX = js.Horizontal;
            float InputY = js.Vertical;
            movement = new Vector2(InputX, InputY).normalized;

            AddTenPoints();

            if (health == 0)
            {
                Destroy(SpawnerObject);
                playerSpeed = 0f;
                StartCoroutine(DeadAnim());
                await SaveScore("MyCurrentScore",score.ToString());
                HighScoreUpdate();
                //SceneManager.LoadScene("Game Over");
            }
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * playerSpeed, movement.y * playerSpeed);
    }
    public void AddTenPoints()
    {
        if(health > 0)
        {
            score += 100 * Time.deltaTime;
            ScoreText.text = ((int)score).ToString();
        }
        else if( health == 0)
        {
            PlayerPrefs.SetFloat("CurrentScore", score);
            ScoreText.text = ((int)score).ToString();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") && isPowered == false && isHit == false)
        {
            StartCoroutine(FlashCo());
        }
        else if(other.CompareTag("Power"))
        {
            StartCoroutine(FlashPower());
        }
    }

    public async void HighScoreUpdate()
    {
        int CurScore = await GetScore<int>("MyCurrentScore");
        int HiScore = await GetScore<int>("MyHighScore");
        if(CurScore>HiScore)
        {
            await SaveScore("MyHighScore",CurScore.ToString());
        }
    }

    private async Task<T> GetScore<T>(string key)
    {
        try
        {
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(
                new HashSet<string> { key }
            );

            if (data.TryGetValue(key, out var item))
            {
                return item.Value.GetAs<T>();
            }
            else
            {
                Debug.Log($"There is no such key as {key}!");
            }
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
        return default;
    }

    private async Task SaveScore(string key, string value)
    {
        try
        {
            var data = new Dictionary<string, object>{{key, value}};
            await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }    
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        //triggerCollider.enabled = false;
        isHit = true;
        while (temp < numberOfFlashes)
        {
            playerSprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            playerSprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        //triggerCollider.enabled = true;
        isHit = false;
    }
    private IEnumerator FlashPower()
    {
        int temp = 0;
        //triggerCollider.enabled = false;
        isPowered = true;
        while (temp < numberOfFlashesPower)
        {
            playerSprite.color = flashColorPower;
            yield return new WaitForSeconds(flashDurationPower);
            playerSprite.color = regularColor;
            yield return new WaitForSeconds(flashDurationPower);
            temp++;
        }
        //triggerCollider.enabled = true;
        isPowered = false;
    }
    private IEnumerator DeadAnim()
    {
        ScorePop.SetTrigger("Dead");
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync("Game Over");
    }
}