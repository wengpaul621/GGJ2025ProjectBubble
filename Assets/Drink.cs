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

    public float intensity;
    public float frequency;


    public void Reset()
    {
        shakeBar.SetMaxShakePressure(maxPressure);

        capacity = maxCapacity;
        capacityBar.SetMaxCapacity(maxCapacity);
        pressure = 0f;
    }

    private void Start()
    {
        Reset();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();

        // Start monitoring mouse speed when left mouse button is pressed
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            
            StartMonitoringMouseSpeed();
        }

        // Monitor mouse speed if active
        if (isMonitoring)
        {
            MonitorMouseSpeed();
            //CameraShake.Instance.shakeCameraWithFrequency(intensity, frequency);
            
        }
        else
        {
            // Gradually decrease the speed when not monitoring (smoothly decrease to 0)
            pressure = Mathf.Lerp(pressure, 0f, Time.deltaTime * 3f);
            shakeBar.SetShakePressure(pressure);
        }
        UpdateCapacity();


    }

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

        // 使用全局坐标系移动
        transform.parent.Translate(Vector3.up * moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }


    void Rotate()
    {
        float rotationSpeed = 100f; // Rotation speed in degrees per second


        switch (playerType)
        {
            case PlayerType.Player1:
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
                }
                break;

            case PlayerType.Player2:
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
                }
                break;
        }

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
            
            if(pressure>=maxPressure)
            {
                pressure = maxPressure;
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
            //Shoot(pressure);
        }
    }

    // Function to update capacity
    private void UpdateCapacity()
    {
        // Reduce capacity based on pressure
        if (pressure > 0)
        {
            float reductionRate = pressure / maxPressure; // Proportional reduction
            capacity -= reductionRate * Time.deltaTime * 1000; // Scale by a factor of 100

            // Ensure capacity doesn't go below 0
            if (capacity < 0)
            {
                capacity = 0;
            }

            capacityBar.SetCapacity(capacity);
        }
    }
}
