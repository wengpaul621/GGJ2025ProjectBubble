using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    // Start is called before the first frame update
    public Defend player;
    private int effect;
    private float speedUpDuration = 2f;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Circle"))
        {
            Debug.Log("take");
            Destroy(collision.gameObject);
            effect = 1;
            if (effect > 0)
            {
                StartCoroutine(ApplyBuff());
            }
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
        player.SetMoveSpeed(5f);
    }

    public void RemoveSpeedUp()
    {
        player.SetMoveSpeed(3f);
    }
    // Update is called once per frame
    public void Update()
    {
        
    }

    
}
