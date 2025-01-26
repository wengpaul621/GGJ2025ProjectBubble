using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Countdowner : MonoBehaviour
{
    public float countdownTime;
    public UnityEvent onTimeout;

    public void Update()
    {
        countdownTime -= Time.deltaTime;

        if(countdownTime <= 0.0f)
        {
            onTimeout.Invoke();
            this.enabled = false;
        }
    }

    public void Event_Destroy(GameObject obj)
    {
        Destroy(obj);
    }
}
