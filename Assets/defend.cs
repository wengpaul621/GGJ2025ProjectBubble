using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Drink;

public class defend : MonoBehaviour
{
    public float moveSpeed = 3f; // 移動速度

    private Vector2 movement;
    public PlayerType playerType;
    void Start()
    {
        SetMoveSpeed( moveSpeed);
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
        // 取得玩家輸入
        



       
        movement = movement.normalized;
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
