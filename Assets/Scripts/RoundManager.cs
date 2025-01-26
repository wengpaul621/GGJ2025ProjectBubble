using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public enum AttackSide
    {
        Player1,
        Player2
    }

    public Generater gene;

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

    public LineMove line;
    bool isGaming=false;

    public Animator animatorAnnouncement;

    public GameObject geneGameObjectP1;
    public GameObject geneGameObjectP2;

    public Vector3 genePositionP1;
    public Vector3 genePositionP2;

    public static RoundManager instance;
    
    
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
    bool ifFirstRound = true;
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
        }
    }
    bool isGameEnd = false;
    void GameEnd()
    {
        if (isGameEnd == true) return;

        // Stop the countdown coroutine
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null; // Ensure the coroutine reference is cleared
        }

        isGameEnd = true;
        Generater.instance.ClearPreviousItems();
        // Determine the winner and display the result
        if (P1Defend.currentHealth > 0)
        {
            
            genePositionP2 = P2Defend.transform.position;
            P2Defend.gameObject.SetActive(false);
            Instantiate(geneGameObjectP2, genePositionP2, Quaternion.identity);
            
            StartCoroutine(AnimateDelay(3f, "P1Win")); // Load scene after a 2-second delay
            StartCoroutine(LoadSceneWithDelay(6f)); // Load scene after a 2-second delay
        }
        else if (P2Defend.currentHealth > 0)
        {
            genePositionP1 = P1Defend.transform.position;
            P1Defend.gameObject.SetActive(false);
            Instantiate(geneGameObjectP1, genePositionP1, Quaternion.identity);
            StartCoroutine(AnimateDelay(3f, "P2Win")); // Load scene after a 2-second delay
            StartCoroutine(LoadSceneWithDelay(6f)); // Load scene after a 2-second delay
        }
    }

    // Coroutine to load the scene with a delay
    private IEnumerator LoadSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0); // Load the scene after the specified delay
    }

    private IEnumerator AnimateDelay(float delay, string triggerName)
    {
        yield return new WaitForSeconds(delay);
        animatorAnnouncement.SetTrigger(triggerName);
    }
    public void ResetGame()
    {
        gene.SwitchRound();
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        P1Defend.Reset();
        P2Defend.Reset();
        Player1Drink.Reset();
        Player2Drink.Reset();
        Generater.instance.ClearPreviousItems();
    }



    public void Player1End()
    {
        Debug.Log("P1End");
        countdownText.gameObject.SetActive(false);
        ResetGame();
        animatorAnnouncement.SetTrigger("P2Turn");

        AnimationEnd[] ends = animatorAnnouncement.GetBehaviours<AnimationEnd>();
        foreach(var end in ends)
        {
            end.endAction += Playe2Round;
        }
    }

    public void Player2End()
    {
        Debug.Log("P2End");
        countdownText.gameObject.SetActive(false);
        ResetGame();
        animatorAnnouncement.SetTrigger("P1Turn");
        AnimationEnd[] ends = animatorAnnouncement.GetBehaviours<AnimationEnd>();
        foreach (var end in ends)
        {
            end.endAction += Playe1Round;
        }


    }

    public void GameStart()
    {
        if (ifFirstRound == true)
        {
            animatorAnnouncement.SetTrigger("P1Turn");

            AnimationEnd[] ends = animatorAnnouncement.GetBehaviours<AnimationEnd>();
            foreach (var end in ends)
            {
                end.endAction += Playe1Round;
            }
            ifFirstRound = false;
        }
    }
    void Playe1Round()
    {

        AnimationEnd[] ends = animatorAnnouncement.GetBehaviours<AnimationEnd>();
        foreach (var end in ends)
        {
            end.endAction -= Playe1Round;
        }
        isGaming = true;
        FieldPlayer1.SetActive(true);
        FieldPlayer2.SetActive(false);
        timer = countdownTime;
        line.gameObject.SetActive(true);
        line.ReturnToOriginalPosition();
        countdownText.gameObject.SetActive(true);

        Generater.instance.GenerateItems();
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
        line.MoveToTarget();
        Generater.instance.GenerateItems();
        countdownText.gameObject.SetActive(true);
        // Stop previous countdown and start a new one
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(CountdownRoutine());
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



    void UpdateCountdownDisplay()
    {
        countdownText.text = timer.ToString("00.00"); // Display as two digits with two decimals (e.g., 09.99, 08.00)
    }

    void CountdownFinished()
    {
        Debug.Log("Countdown finished!");
        if (attackSide == AttackSide.Player1)
        {
            Player1End();
            //StartCoroutine(StartTimer(break_time, 0));
        }
        else if (attackSide == AttackSide.Player2)
        {
            Player2End();
            //StartCoroutine(StartTimer(break_time, 1));
        }
    }
}
