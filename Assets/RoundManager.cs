using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    // Enum for player type
    public enum AttackSide
    {
        Player1,
        Player2
    }
    public AttackSide attackSide;
    public GameObject FieldPlayer1;
    public GameObject FieldPlayer2;

    public Drink Player1Drink;
    public Drink Player2Drink;

    public TextMeshProUGUI countdownText;
    public int countdownTime = 10; // Set initial countdown time in seconds

    private float timer;

    public float break_time = 3f;
    public LineMove line;
    bool isGaming=false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        //Playe1Round();
        //timer = countdownTime;
        //StartCoroutine(CountdownRoutine());
    }

    // Update is called once per frame
    void Update()
    {

        if (!isGaming) { return; }
        if (attackSide == AttackSide.Player1 && Player1Drink.capacity <= 0)
        {
           Player1End();
        }

        if (attackSide == AttackSide.Player2 && Player2Drink.capacity <= 0)
        {
            Player2End();
        }
    }

    public void Playe1Round()
    {
        isGaming = true;
        
        FieldPlayer1.SetActive(true);
        FieldPlayer2.SetActive(false);
        timer = countdownTime; // Reset the timer for the new round
        line.gameObject.SetActive(true);
        countdownText.gameObject.SetActive(true);
        StartCoroutine(CountdownRoutine());
    }

    public void Player1End()
    {
        Debug.Log("Start");
        Playe2Round();
    }

    void Playe2Round()
    {
        
        FieldPlayer1.SetActive(false);
        FieldPlayer2.SetActive(true);
        timer = countdownTime; // Reset the timer for the new round
        StartCoroutine(CountdownRoutine());
    }

    public void Player2End()
    {
        Playe1Round();
    }

    IEnumerator CountdownRoutine()
    {
        while (timer > 0)
        {
            UpdateCountdownDisplay();
            yield return new WaitForSeconds(0.01f); // Adjusted for finer granularity
            timer -= 0.01f;
        }

        // When the countdown reaches 0, update the display and trigger an event
        timer = 0; // Ensure timer doesn't go negative
        UpdateCountdownDisplay();
        CountdownFinished();
    }

    void UpdateCountdownDisplay()
    {
        countdownText.text = timer.ToString("00.00"); // Display as two digits with two decimals (e.g., 09.99, 08.00)
    }

    void CountdownFinished()
    {
        Debug.Log("Countdown finished!");
        if (attackSide == AttackSide.Player1)
        {
            StartCoroutine(StartTimer(break_time, 0));
        }
        else if (attackSide == AttackSide.Player2)
        {
            StartCoroutine(StartTimer(break_time, 1));
        }
    }

    IEnumerator StartTimer(float duration, int player)
    {
        Debug.Log("Timer started...");
        isGaming = false;
        if (player == 0)
        {
            attackSide = AttackSide.Player2;
        }
        else
        {
            attackSide = AttackSide.Player1;
        }
        yield return new WaitForSeconds(duration);
        Debug.Log("3 seconds passed!");
        isGaming = true;
        if (player == 0)
        {
            Player1End();
        }
        else
        {
            Player2End();
        }
        // Add actions to execute after the timer ends
    }
}
