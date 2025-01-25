using DG.Tweening;
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

    public Defend P1Defend;
    public Defend P2Defend;

    public Drink Player1Drink;
    public Drink Player2Drink;

    public TextMeshProUGUI countdownText;
    public int countdownTime = 10; // Set initial countdown time in seconds

    private float timer;
    private AnimatorStateInfo info;
    public LineMove line;
    bool isGaming=false;

    public Animator animatorAnnouncement;

    public GameObject geneGameObjectP1;
    public GameObject geneGameObjectP2;

    public Vector3 genePositionP1;
    public Vector3 genePositionP2;
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
        // Handle game state based on the attack side
        switch (attackSide)
        {
            case AttackSide.Player1:
                if (Player1Drink.capacity <= 0)
                {
                    Player1End(); // Player 2 wins if Player 1's drink is empty
                }
                else if (P2Defend.currentHealth <= 0)
                {
                    Debug.Log("P2GG");
                    GameEnd();
                }
                break;

            case AttackSide.Player2:
                if (Player2Drink.capacity <= 0)
                {
                    Player2End(); // Player 1 wins if Player 2's drink is empty
                }
                else if (P1Defend.currentHealth <= 0)
                {
                    GameEnd();
                }
                break;

            default:
                Debug.LogWarning("Unknown attack side detected!"); // Fallback case for debugging
                break;
        }
    }
    bool isGameEnd = false;
    void GameEnd()
    {
        if (isGameEnd == true) return;

        isGameEnd = true;
        if(P1Defend.currentHealth > 0)
        {
            animatorAnnouncement.SetTrigger("P1Win");
            Instantiate(geneGameObjectP2, genePositionP2, Quaternion.identity);

        }
        else if(P2Defend.currentHealth > 0)
        {
            animatorAnnouncement.SetTrigger("P2Win");
            Instantiate(geneGameObjectP2, genePositionP1, Quaternion.identity);
        }
    }
    public void ResetGame()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        P1Defend.Reset();
        P2Defend.Reset();
        Player1Drink.Reset();
        Player2Drink.Reset();
        
    }



    public void Player1End()
    {
        Debug.Log("P1End");
        ResetGame();
        animatorAnnouncement.SetTrigger("P2Turn");

        AnimationEnd[] ends = animatorAnnouncement.GetBehaviours<AnimationEnd>();
        foreach(var end in ends)
        {
            end.endAction += Playe2Round;
        }

        //info = animatorAnnouncement.GetCurrentAnimatorStateInfo(0);
        //if (info.normalizedTime >= 1) // 判断动画播放结束normalizedTime的值为0~1，0为开始，1为结束。
        //{
        //    Playe2Round();
        //}
       
    }


    public void Player2End()
    {
        Debug.Log("P2End");
        ResetGame();
        animatorAnnouncement.SetTrigger("P1Turn");
        info = animatorAnnouncement.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1) // 判断动画播放结束normalizedTime的值为0~1，0为开始，1为结束。
        {
            Playe1Round();
        }

        
    }

    private Coroutine countdownCoroutine;

    IEnumerator CountdownRoutine()
    {
        while (timer > 0)
        {
            UpdateCountdownDisplay();
            yield return new WaitForSeconds(0.01f);
            timer -= 0.01f;
        }

        // When the countdown reaches 0, update the display and trigger an event
        timer = 0; // Ensure timer doesn't go negative
        UpdateCountdownDisplay();
        CountdownFinished();
    }

    void Playe1Round()
    {
        isGaming = true;
        attackSide = AttackSide.Player1;
        FieldPlayer1.SetActive(true);
        FieldPlayer2.SetActive(false);
        timer = countdownTime;
        line.gameObject.SetActive(true);
        countdownText.gameObject.SetActive(true);

        // Stop previous countdown and start a new one
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(CountdownRoutine());
    }

    void Playe2Round()
    {
        AnimationEnd[] ends = animatorAnnouncement.GetBehaviours<AnimationEnd>();
        foreach (var end in ends)
        {
            end.endAction -= Playe2Round;
        }

        Debug.Log("P2Round");
        attackSide = AttackSide.Player2;
        FieldPlayer1.SetActive(false);
        FieldPlayer2.SetActive(true);
        timer = countdownTime;

        // Stop previous countdown and start a new one
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(CountdownRoutine());
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
            Player1End(); // Switch to Player 2
        }
        else if (attackSide == AttackSide.Player2)
        {
            Player2End(); // Switch to Player 1
        }
    }


}
