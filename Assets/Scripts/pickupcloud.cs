using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupcloud : MonoBehaviour
{
    float wallDuration = 5f;
    public GameObject wallPrefab; // Prefab for the wall

    public Vector2 Player_1_minXY;
    public Vector2 Player_1_maxXY;
    public Vector2 Player_2_minXY;
    public Vector2 Player_2_maxXY;
    public Defend player;
    // Start is called before the first frame update

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("take");

            // Get the Defend component from the player
            player = collision.gameObject.GetComponent<Defend>();

            // Start the ApplyBuff coroutine
            GenerateWall();

            // Disable the SpriteRenderer and Collider2D components
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            Collider2D collider = gameObject.GetComponent<Collider2D>();

            if (spriteRenderer != null)
                spriteRenderer.enabled = false;

            if (collider != null)
                collider.enabled = false;
            Destroy(gameObject);
            // Optionally, you can also destroy the game object or disable it entirely if needed
            // Destroy(gameObject); // Or gameObject.SetActive(false);
        }
    }


    public enum AttackSide
    {
        Player1,
        Player2
    }
    public AttackSide attackSide;  // 当前的攻击方
    Vector3 GetRandomSpawnPosition()
    {
        if (attackSide == AttackSide.Player1)
        {
            return new Vector3(Random.Range(Player_2_minXY.x, Player_2_maxXY.x),
                               Random.Range(Player_2_minXY.y, Player_2_maxXY.y),
                               0f);
        }
        else
        {
            return new Vector3(Random.Range(Player_1_minXY.x, Player_1_maxXY.x),
                               Random.Range(Player_1_minXY.y, Player_1_maxXY.y),
                               0f); 
        }
    }

    private void GenerateWall()
    {
        // Create a new wall from the prefab
        GameObject wall = Instantiate(wallPrefab);

        // Generate random size for the wall


        // Generate random position for the wall


        // Apply size and position
        wall.transform.position = GetRandomSpawnPosition();

        // Destroy the wall after a delay
        Destroy(wall, wallDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
