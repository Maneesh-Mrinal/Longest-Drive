using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeInvincible : MonoBehaviour
{
    public float speed;
    public float powerTime = 5f;
    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerMovement>().isPowered == false)
        {
            //Debug.Log("Collision Occurred");
            other.GetComponent<PlayerMovement>().isPowered = true;
            Destroy(gameObject);
            //Debug.Log(other.GetComponent<PlayerMovement>().isPowered);
            powerTime -= Time.deltaTime;
            if (powerTime == 0)
            {
                other.GetComponent<PlayerMovement>().isPowered = false;
            }
        }
    }
}
