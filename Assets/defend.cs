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

    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth); // Ensure this is implemented in your HealthSystem class
        SetMoveSpeed(moveSpeed);
    }

    void Update()
    {
        movement.x = 0f;
        movement.y = 0f;

        // Handle movement based on player type
        switch (playerType)
        {
            case PlayerType.Player1:
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    movement.y += 1;
                }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    movement.y -= 1;
                }
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    movement.x -= 1;
                }
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    movement.x += 1;
                }
                break;

            case PlayerType.Player2:
                if (Input.GetKey(KeyCode.PageUp))
                {
                    movement.y += 1;
                }
                if (Input.GetKey(KeyCode.PageDown))
                {
                    movement.y -= 1;
                }
                if (Input.GetKey(KeyCode.Home))
                {
                    movement.x -= 1;
                }
                if (Input.GetKey(KeyCode.End))
                {
                    movement.x += 1;
                }
                break;
        }

        // Normalize movement vector to ensure consistent speed
        movement = movement.normalized;

        // Handle damage input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Damage triggered!");
            Damage(20f);
        }
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
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}

// Enum for PlayerType (ensure this is defined elsewhere in your project if needed)
public enum PlayerType
{
    Player1,
    Player2
}

// Ensure your HealthSystem class is implemented correctly
