using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float random_x = Random.Range(0.5f, 3f);
        float random_y = Random.Range(0.5f, 3f);
        this.transform.localScale = new Vector3(random_x, random_y, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyWall()
    {
        Destroy(this);
    }
}
