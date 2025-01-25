using System.Collections;
using UnityEngine;
using DG.Tweening;
using static RoundManager;

public class LineMove : MonoBehaviour
{
    public Vector3 targetPosition;
    public float moveDuration = 1f; // 移动到目标位置的时间

    private Vector3 originalPosition; // 原始位置

    // Start is called before the first frame update
    void Start()
    {
        // 记录初始位置
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        // 根据攻击方决定移动方向
        if (RoundManager.instance.attackSide == AttackSide.Player2 && transform.position != targetPosition)
        {
            MoveToTarget();
        }
        else if (RoundManager.instance.attackSide == AttackSide.Player1 && transform.position != originalPosition)
        {
            ReturnToOriginalPosition();
        }
    }

    void MoveToTarget()
    {
        transform.DOLocalMove(targetPosition, moveDuration); // 移动到目标位置
    }

    void ReturnToOriginalPosition()
    {
        transform.DOLocalMove(originalPosition, moveDuration); // 返回原始位置
    }
}
