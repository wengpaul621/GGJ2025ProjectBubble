using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCollider : MonoBehaviour
{
    public GameObject hitEffectPrefab;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bubble"))
        {
            Instantiate(hitEffectPrefab, collision.collider.ClosestPoint(collision.transform.position), Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
