using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupcloud : MonoBehaviour
{
    float wallDuration = 3f;
    public GameObject wallPrefab; // Prefab for the wall
    public GameObject player1Corner1;
    public GameObject player1Corner2;
    public GameObject player2Corner1;
    public GameObject player2Corner2;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cloud"))
        {

            Debug.Log("take cloud");
            Destroy(collision.gameObject);
            GenerateWall();
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
            return new Vector3(Random.Range(player2Corner1.transform.position.x, player2Corner2.transform.position.x),
                               Random.Range(player2Corner1.transform.position.y, player2Corner2.transform.position.y),
                               0f);
        }
        else
        {
            return new Vector3(Random.Range(player1Corner1.transform.position.x, player1Corner2.transform.position.x),
                               Random.Range(player1Corner1.transform.position.y, player1Corner2.transform.position.y),
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
