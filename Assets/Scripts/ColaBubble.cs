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

    Tween scaleTransisTween;
    Tween disableColliderTween;

    public float damage=5;
    private void Awake()
    {
        float lifetime = Random.Range(lifetimeRange.x, lifetimeRange.y);
        float disableColliderTime = lifetime * disableColliderTimePerc;
        scaleTransisTween = DOVirtual.Float(0.0f, 1.0f, lifetime, (f) => { transform.localScale = Vector3.one * scaleTransisCurve.Evaluate(f); })
            .OnComplete(() => Destroy(gameObject));
        disableColliderTween = DOVirtual.DelayedCall(disableColliderTime, () => { coll.enabled = false; });
    }

    private void OnDestroy()
    {
        if (scaleTransisTween != null) scaleTransisTween.Kill(false);
        if (disableColliderTween != null) disableColliderTween.Kill(false);
    }
}
