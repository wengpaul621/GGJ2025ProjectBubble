using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetMaxShakePressure(float pressure)
    {
        slider.maxValue = pressure;
        //slider.value = speed;
        //fill.color = gradient.Evaluate(1f);
    }
    public void SetShakePressure(float pressure)
    {
        slider.value = pressure;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
