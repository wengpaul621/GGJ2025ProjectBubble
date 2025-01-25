using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defend : MonoBehaviour
{
    public float moveSpeed = 3f; // 移動速度

    private Vector2 movement;

    void Start()
    {
        SetMoveSpeed(3f);
    }

    void Update()
    {
        // 取得玩家輸入
        movement.x = 0f; // 初始化
        movement.y = 0f;

        // 使用 W、A、S、D 控制方向
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

        // 規範移動量 (避免斜向移動過快)
        movement = movement.normalized;
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
