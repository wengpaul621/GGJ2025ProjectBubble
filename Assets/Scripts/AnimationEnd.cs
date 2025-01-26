using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnd : StateMachineBehaviour
{
    [SerializeField]public System.Action endAction;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //endAction += AAA;
        if (stateInfo.normalizedTime >= 1.0f)
        {
            if (endAction != null) endAction.Invoke();
        }
    }
    //public void AAA()
    //{

    //}
}
