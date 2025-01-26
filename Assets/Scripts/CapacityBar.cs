using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapacityBar : MonoBehaviour
{
    public Image fill;

    private float maxCapacity;

    public void SetMaxCapacity(float capacity)
    {
        maxCapacity = capacity;
    }
    public void SetCapacity(float capacity)
    {
        fill.fillAmount = capacity / maxCapacity;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}