using System.Collections;
using UnityEngine;

public class Drink : MonoBehaviour
{
    // Enum for player type
    public enum PlayerType
    {
        Player1,
        Player2
    }

    // Public variable to select player type from dropdown in Inspector
    public PlayerType playerType;

    public float moveSpeed = 5f;

    private Vector3 lastMousePosition;
    private bool isMonitoring = false;
    private float totalDistance = 0f;
    private float elapsedTime = 0f;
    public float updateInterval = 0.1f; // Update average speed every 0.1 seconds
    public float averageShakeSpeed = 0f;

    public float pressure;
    public float maxPressure = 30000;
    public float speedRangeLow = 3000;
    public float speedRangeHeight = 5000;

    public ShakeBar shakeBar;
    public CapacityBar capacityBar;

    public float maxCapacity = 5000;
    public float capacity = 0f;
    private void Start()
    {
        shakeBar.SetMaxShakePressure(maxPressure);

        capacity = maxCapacity;
        capacityBar.SetMaxCapacity(maxCapacity);

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // Start monitoring mouse speed when left mouse button is pressed
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            StartMonitoringMouseSpeed();
        }

        // Monitor mouse speed if active
        if (isMonitoring)
        {
            MonitorMouseSpeed();
            
        }
        else
        {
            // Gradually decrease the speed when not monitoring (smoothly decrease to 0)
            pressure = Mathf.Lerp(pressure, 0f, Time.deltaTime * 2f);
            shakeBar.SetShakePressure(pressure);
        }
        
    }

    // Move logic (Player movement controls can be added here)
    void Move()
    {
        float moveDirection = 0f;

        switch (playerType)
        {
            case PlayerType.Player1:
                if (Input.GetKey(KeyCode.W))
                {
                    moveDirection = 1f;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    moveDirection = -1f;
                }
                break;

            case PlayerType.Player2:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    moveDirection = 1f;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    moveDirection = -1f;
                }
                break;
        }

        transform.Translate(Vector3.up * moveDirection * moveSpeed * Time.deltaTime);
    }

    // Function to start mouse speed monitoring
    private void StartMonitoringMouseSpeed()
    {
        isMonitoring = true;
        
        lastMousePosition = Input.mousePosition; // Record initial position
        totalDistance = 0f;
        elapsedTime = 0f;
    }

    // Function to monitor and calculate mouse speed
    private void MonitorMouseSpeed()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        float distance = Vector3.Distance(currentMousePosition, lastMousePosition);

        // 忽略小移动的逻辑
        float distanceThreshold = 0.1f;
        if (distance > distanceThreshold)
        {
            totalDistance += distance;
            elapsedTime += Time.deltaTime;
        }
        else
        {
            // 鼠标静止时，通过时间增加压力
            pressure -= Time.deltaTime * 5000f;
            shakeBar.SetShakePressure(pressure);
        }

        // 更新平均速度
        if (elapsedTime >= updateInterval)
        {
            float newShakeSpeed = totalDistance / elapsedTime;
            averageShakeSpeed = Mathf.Lerp(averageShakeSpeed, newShakeSpeed, 0.5f);
            Debug.Log("Smoothed Average Mouse Speed: " + averageShakeSpeed.ToString("F2") + " pixels/second");

            // 计算压力
            float deltaPressure = Mathf.Sqrt(averageShakeSpeed);
            if (averageShakeSpeed >= speedRangeHeight)
            {
                deltaPressure = 10 * Mathf.Sqrt(averageShakeSpeed);
                pressure += deltaPressure;
            }
            else if (averageShakeSpeed < speedRangeLow)
            {
                deltaPressure = 10 * Mathf.Sqrt(averageShakeSpeed);
                pressure -= deltaPressure;
            }
            
            shakeBar.SetShakePressure(pressure);
            deltaPressure = 0;
            totalDistance = 0f;
            elapsedTime = 0f;
        }

        lastMousePosition = currentMousePosition;

        // 停止监测
        if (Input.GetMouseButtonUp(0))
        {
            isMonitoring = false;
            Shoot(pressure);
        }
    }

    void Shoot(float pressure)
    {

    }

   
}
