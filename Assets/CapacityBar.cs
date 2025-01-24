using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapacityBar : MonoBehaviour
{
    public Slider slider;
    //public Gradient gradient;
    public Image fill;
    public void SetMaxCapacity(float capacity)
    {
        slider.maxValue = capacity;
        slider.value = capacity;
        //fill.color = gradient.Evaluate(1f);
    }
    public void SetCapacity(float capacity)
    {
        slider.value = capacity;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}