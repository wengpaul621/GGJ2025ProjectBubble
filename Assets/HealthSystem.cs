using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthBar;
    void Start()
    {
        

    }

    public void SetMaxHealth(float health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    // Update is called once per frame
    public void SetHealth(float health)
    {
        healthBar.value = health;
    }
}
