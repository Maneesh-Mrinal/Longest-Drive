using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public float score;
    public Animator ScorePop;
    public int HighScore;
    public float timeLeft = 3.0f;
    public FloatingJoystick js;
    public bool isHit = false;
    public bool isPowered = false;
    public GameObject SpawnerObject;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            HealthText.text = health.ToString();

            float InputX = js.Horizontal;
            float InputY = js.Vertical;
            movement = new Vector2(InputX, InputY).normalized;

            AddPoints();

            if (health == 0)
            {
                Destroy(SpawnerObject);
                playerSpeed = 0f;
                LoadPlayerHighScore();
                if( (int)score > HighScore)
                {
                    HighScore = (int)score;
                }
                SavePlayer();
                StartCoroutine(DeadAnim());
                //SceneManager.LoadScene("Game Over");
            }
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * playerSpeed, movement.y * playerSpeed);
    }
    public void AddPoints()
    {
        if(health > 0)
        {
            score += 50 * Time.deltaTime;
            ScoreText.text = ((int)score).ToString();
        }
        else if( health == 0)
        {
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

    public void SavePlayer ()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer ()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        HighScore = data.highScore;
        score = data.currentScore;
    }
    public void LoadPlayerHighScore()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        HighScore = data.highScore;
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