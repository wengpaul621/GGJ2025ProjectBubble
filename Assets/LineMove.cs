using System.Collections;
using UnityEngine;
using DG.Tweening;
using static RoundManager;

public class LineMove : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 middlePosition;
    public float moveDuration = 1f; // 移动到目标位置的时间
    public float stayDuration = 2f;
    private Vector3 originalPosition; // 原始位置

    // Start is called before the first frame update
    void Start()
    {
        // 记录初始位置
        originalPosition = transform.localPosition;
    }

    int LtoR = 0;
    int RtoL = 0;
    bool firstTime = true;
    private void Update()
    {
        // 根据攻击方决定移动方向
        if (RoundManager.instance.attackSide == AttackSide.Player2 && transform.position != targetPosition)
        {
            LtoR++;
            RtoL = 0;
            firstTime = false;
            if (LtoR == 1)
            {
                StartCoroutine(LeftToRight());
            }
        }
        else if (RoundManager.instance.attackSide == AttackSide.Player1 && transform.position != originalPosition && firstTime == true)
        {
            ReturnToOriginalPosition();
        }
        else if (RoundManager.instance.attackSide == AttackSide.Player1 && transform.position != originalPosition && firstTime == false)
        {
            RtoL++;
            LtoR = 0;
            if (RtoL == 1)
            {
                StartCoroutine(RightToLeft());
            }
        }
    }


    public IEnumerator LeftToRight()
    {
        Debug.Log("Left to Right");
        MoveToMiddle();
        yield return new WaitForSeconds(stayDuration);
        MoveToTarget();
    }

    public IEnumerator RightToLeft()
    {
        Debug.Log("Right to Left");
        MoveToMiddle();
        yield return new WaitForSeconds(stayDuration);
        ReturnToOriginalPosition();
    }

    void MoveToMiddle()
    {
        transform.DOLocalMove(middlePosition, moveDuration); // 移动到目标位置
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
