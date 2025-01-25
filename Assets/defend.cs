using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defend : MonoBehaviour
{
    public float moveSpeed = 3f; // 移動速度
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
        // 取得玩家輸入
        movement.x = 0f; // 初始化
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

        // 規範移動量 (避免斜向移動過快)
        movement = movement.normalized;
    }

    void Damage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);   
    }
    void FixedUpdate()
    {
        // 更新物體位置
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
