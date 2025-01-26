using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    // Start is called before the first frame update
    public RoundManager round;
    public Defend player;
    private int effect;


    //Speed up period
    private float speedUpDuration = 2f;

    public Animator animator;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("take");

            // Get the Defend component from the player
            player = collision.gameObject.GetComponent<Defend>();

            // Start the ApplyBuff coroutine
            StartCoroutine(ApplyBuff());

            // Disable the SpriteRenderer and Collider2D components
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            Collider2D collider = gameObject.GetComponent<Collider2D>();

            if (spriteRenderer != null)
                spriteRenderer.enabled = false;

            if (collider != null)
                collider.enabled = false;

            // Optionally, you can also destroy the game object or disable it entirely if needed
            // Destroy(gameObject); // Or gameObject.SetActive(false);
        }
    }


    public IEnumerator ApplyBuff()
    {


        // Example: Apply buff logic here (e.g., increase speed)
        Debug.Log("Buff Activated!");
        SpeedUpApply();

        // Wait for the buff duration
        yield return new WaitForSeconds(speedUpDuration);

        // Remove the buff
        RemoveSpeedUp();
        Debug.Log("Buff Deactivated!");

    }

    public void SpeedUpApply()
    {
        player.SetMoveSpeed(7f);
    }

    public void RemoveSpeedUp()
    {
        player.SetMoveSpeed(5f);
        Destroy(gameObject);
    }

    
}
