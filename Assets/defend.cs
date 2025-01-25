using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defend : MonoBehaviour
{
    public float moveSpeed = 3f; // ���ʳt��
    public float currentHealth;
    public float maxHealth;

    public HealthSystem healthBar;
    private Vector2 movement;

    void Start()
    {
        maxHealth = 100;
        SetMoveSpeed(3f);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        // ���o���a��J
        movement.x = 0f; // ��l��
        movement.y = 0f;

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

        if (Input.GetKeyDown(KeyCode.Space))    
        {
            Debug.Log("damage");
            Damage(20f);
        }

        // �W�d���ʶq (�קK�צV���ʹL��)
        movement = movement.normalized;
    }

    void Damage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);   
    }
    void FixedUpdate()
    {
        // ��s�����m
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
