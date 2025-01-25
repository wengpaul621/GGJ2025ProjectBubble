using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : MonoBehaviour
{
    public float moveSpeed = 3f; // Movement speed
    public float currentHealth;
    public float maxHealth;

    public HealthSystem healthBar;
    private Vector2 movement;
    public PlayerType playerType;
    public GameObject hitEffectPrefab;
    public Animator animator;
    public float damage = 5;

    public void Reset()
    {
        SetMoveSpeed(moveSpeed);
    }
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth); // Ensure this is implemented in your HealthSystem class
        Reset();
    }

    void Update()
    {
        movement.x = 0f;
        movement.y = 0f;

        // Handle movement based on player type
        switch (playerType)
        {
            case PlayerType.Player1:
                if (Input.GetKey(KeyCode.W) )
                {
                    movement.y += 1;
                }
                if (Input.GetKey(KeyCode.S) )
                {
                    movement.y -= 1;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    movement.x -= 1;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    movement.x += 1;
                }
                break;

            case PlayerType.Player2:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    movement.y += 1;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    movement.y -= 1;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    movement.x -= 1;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    movement.x += 1;
                }
                break;
        }

        // Normalize movement vector to ensure consistent speed
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Move the player
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0; // Prevent health from going negative
        healthBar.SetHealth(currentHealth); // Update the health bar
        animator.SetTrigger("Hurt");
        
        
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bubble"))
        {
            Damage(damage);
            Instantiate(hitEffectPrefab, collision.collider.ClosestPoint(collision.transform.position), Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}

// Enum for PlayerType (ensure this is defined elsewhere in your project if needed)
public enum PlayerType
{
    Player1,
    Player2
}
