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
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer playerSprite;
    public int score = 0;
    public Animator ScorePop;
    public int HighScore;
    public float timeLeft = 3.0f;
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

            float InputX = Input.GetAxis("Horizontal");
            float InputY = Input.GetAxis("Vertical");
            movement = new Vector2(InputX, InputY).normalized;

            AddTenPoints();

            if (health == 0)
            {
                StartCoroutine(DeadAnim());
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
            score += 1;
            ScoreText.text = score.ToString();
        }
        else if( health == 0)
        {
            ScoreText.text = score.ToString();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            StartCoroutine(FlashCo());
        }
    }
    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while (temp < numberOfFlashes)
        {
            playerSprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            playerSprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
    private IEnumerator DeadAnim()
    {
        ScorePop.SetTrigger("Dead");
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync("Game Over");
    }
}