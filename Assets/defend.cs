using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Drink;

public class defend : MonoBehaviour
{
    public float moveSpeed; // ���ʳt��
    public float currentHealth;
    public float maxHealth;

    public HealthSystem healthBar;
    private Vector2 movement;
    public PlayerType playerType;
    void Start()
    {
        moveSpeed = 3f;
        maxHealth = 100;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        SetMoveSpeed(moveSpeed);
    }

    void Update()
    {
        movement.x = 0f;
        movement.y = 0f;
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
                if (Input.GetKey(KeyCode.PageUp) || Input.GetKey(KeyCode.UpArrow))
                {
                    movement.y += 1;
                }
                if (Input.GetKey(KeyCode.PageDown) || Input.GetKey(KeyCode.DownArrow))
                {
                    movement.y -= 1;
                }
                if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.LeftArrow))
                {
                    movement.x -= 1;
                }
                if (Input.GetKey(KeyCode.End) || Input.GetKey(KeyCode.RightArrow))
                {
                    movement.x += 1;
                }
                break;


        }
        movement = movement.normalized;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("damage");
            Damage(20f);
        }
    }

        void Damage(float damage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
        void FixedUpdate()
        {

            transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
        }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
