using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damage = 1;
    public float speed;
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer playerSprite;

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerMovement>().isHit == 0)
            {
                //player hits = Takes damage !
                other.GetComponent<PlayerMovement>().health -= damage;
                Debug.Log(other.GetComponent<PlayerMovement>().health);
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Health"))
        {
            if(other.GetComponent<PlayerMovement>().health < 3)
            {
                other.GetComponent<PlayerMovement>().health += 1;
                Destroy(gameObject);
            }
        }
    }
}