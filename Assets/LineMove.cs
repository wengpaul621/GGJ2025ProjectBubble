using System.Collections;
using UnityEngine;
using DG.Tweening;
using static RoundManager;

public class LineMove : MonoBehaviour
{
    public Vector3 targetPosition;
    public float moveDuration = 1f; // �ƶ���Ŀ��λ�õ�ʱ��

    private Vector3 originalPosition; // ԭʼλ��

    // Start is called before the first frame update
    void Start()
    {
        // ��¼��ʼλ��
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        // ���ݹ����������ƶ�����
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
        transform.DOLocalMove(targetPosition, moveDuration); // �ƶ���Ŀ��λ��
    }

    void ReturnToOriginalPosition()
    {
        transform.DOLocalMove(originalPosition, moveDuration); // ����ԭʼλ��
    }
}
