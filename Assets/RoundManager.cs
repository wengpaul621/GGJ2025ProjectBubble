using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Drink;

public class RoundManager : MonoBehaviour
{
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

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (attackSide == AttackSide.Player1 &&Player1Drink.capacity <= 0)
        {
            Debug.Log("Player1Lose");
        }

        if (attackSide == AttackSide.Player2 && Player2Drink.capacity <= 0)
        {
            Debug.Log("Player2Lose");
        }
    }

    void Playe1Round()
    {
        FieldPlayer1.SetActive(true);
        FieldPlayer2.SetActive(false);
    }

    public void Player1End()
    {
        Playe2Round();
    }

    void Playe2Round()
    {
        FieldPlayer1.SetActive(false);
        FieldPlayer2.SetActive(true);
    }

    public void Player2End()
    {
        Playe1Round();
    }
}
