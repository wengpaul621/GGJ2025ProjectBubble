using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeBar : MonoBehaviour
{
    public Image fill;
    public Gradient gradient;

    private float maxPressure;
    public void SetMaxShakePressure(float pressure)
    {
        maxPressure = pressure;
        //slider.value = speed;
        //fill.color = gradient.Evaluate(1f);
    }
    public void SetShakePressure(float pressure)
    {
        fill.fillAmount = pressure / maxPressure;
        fill.color = gradient.Evaluate(fill.fillAmount);
    }
}
