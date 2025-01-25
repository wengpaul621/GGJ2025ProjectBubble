using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    // Start is called before the first frame update
    public Defend player;
    private int effect;

    //Genarate Wall
    public GameObject wallPrefab; // Prefab for the wall
    public float minWidth = 1f;   // Minimum width of the wall
    public float maxWidth = 5f;   // Maximum width of the wall
    public float minHeight = 1f;  // Minimum height of the wall
    public float maxHeight = 5f;  // Maximum height of the wall
    public Vector2 minPosition = new Vector2(-5f, -5f); // Minimum position for the wall
    public Vector2 maxPosition = new Vector2(5f, 5f);   // Maximum position for the wall
    public float wallDuration = 2f; // Time (in seconds) the wall stays active

    //Speed up period
    private float speedUpDuration = 2f;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Circle"))
        {
            Debug.Log("take");
            Destroy(collision.gameObject);
            effect = Random.Range(0, 2);
            if (effect > 0)
            {
                StartCoroutine(ApplyBuff());
            }
            else
            {
                GenerateWall();
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

    private void GenerateWall()
    {
        // Create a new wall from the prefab
        GameObject wall = Instantiate(wallPrefab);

        // Generate random size for the wall
        float randomWidth = Random.Range(minWidth, maxWidth);
        float randomHeight = Random.Range(minHeight, maxHeight);

        // Generate random position for the wall
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);

        // Apply size and position
        wall.transform.localScale = new Vector3(randomWidth, randomHeight, 1f);
        wall.transform.position = new Vector3(randomX, randomY, 0f);

        // Destroy the wall after a delay
        Destroy(wall, wallDuration);
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    
}
