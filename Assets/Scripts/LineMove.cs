using System.Collections;
using UnityEngine;
using DG.Tweening;
using static RoundManager;

public class LineMove : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 middlePosition;
    public float moveDuration = 1f; // �ƶ���Ŀ��λ�õ�ʱ��
    public float stayDuration = 2f;
    public Vector3 originalPosition; // ԭʼλ��

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
    }

    int LtoR = 0;
    int RtoL = 0;
    bool firstTime = true;
    private void Update()
    {
        //// ���ݹ����������ƶ�����
        //if (RoundManager.instance.attackSide == AttackSide.Player2 && Vector3.Distance(transform.position, targetPosition)<0.1f)
        //{
        //    //LtoR++;
        //    //RtoL = 0;
        //    //firstTime = false;
        //    //if (LtoR == 1)
        //    //{
        //    //    StartCoroutine(LeftToRight());
        //    //}
        //    MoveToTarget();
        //}
        //else if (RoundManager.instance.attackSide == AttackSide.Player1 && Vector3.Distance(transform.position, originalPosition)<0.1f)
        //{
        //    ReturnToOriginalPosition();
        //}
        ////else if (RoundManager.instance.attackSide == AttackSide.Player1 && transform.position != originalPosition && firstTime == false)
        ////{
        ////    RtoL++;
        ////    LtoR = 0;
        ////    if (RtoL == 1)
        ////    {
        ////        StartCoroutine(RightToLeft());
        ////    }
        ////}
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
        transform.DOLocalMove(middlePosition, moveDuration); // �ƶ���Ŀ��λ��
    }
    public void MoveToTarget()
    {
        transform.DOLocalMove(targetPosition, moveDuration); // �ƶ���Ŀ��λ��
    }

    public void ReturnToOriginalPosition()
    {
        transform.DOLocalMove(originalPosition, moveDuration); // ����ԭʼλ��
    }
}
