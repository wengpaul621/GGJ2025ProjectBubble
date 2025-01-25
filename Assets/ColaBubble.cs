using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ColaBubble : MonoBehaviour
{
    [Title("Component")]
    [SerializeField] private Collider2D coll;

    [Title("Param")]
    [SerializeField] private Vector2 lifetimeRange;

    [Title("Scale Transis")]
    [SerializeField] private AnimationCurve scaleTransisCurve;
    [SerializeField] private float disableColliderTimePerc;

    private void Awake()
    {
        float lifetime = Random.Range(lifetimeRange.x, lifetimeRange.y);
        float disableColliderTime = lifetime * disableColliderTimePerc;
        DOVirtual.Float(0.0f, 1.0f, lifetime, (f) => { transform.localScale = Vector3.one * scaleTransisCurve.Evaluate(f); })
            .OnComplete(() => Destroy(gameObject));
        DOVirtual.DelayedCall(disableColliderTime, () => { coll.enabled = false; });
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Destroy(gameObject);
        }
        
    }
}
