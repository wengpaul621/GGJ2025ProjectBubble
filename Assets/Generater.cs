using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Generater : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] itemPrefabs;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    void Start()
    {
        InvokeRepeating("Timer", 1.0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Timer()
    {
        
        float rand = Random.Range(0f, 500f);
        if (rand <= 8)
        {
            Debug.Log("Start Gene");
            GameObject itemToSpawn = itemPrefabs[0];
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(randomX, randomY);
            Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
