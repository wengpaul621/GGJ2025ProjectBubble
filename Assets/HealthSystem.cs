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
    public float smoothTime = 0.2f;

    // Update the health bar smoothly
    public void SetHealth(float health)
    {
        StartCoroutine(SmoothHealthChange(health));
    }

    private IEnumerator SmoothHealthChange(float targetHealth)
    {
        float currentHealth = healthBar.value;
        float elapsedTime = 0f;

        while (elapsedTime < smoothTime)
        {
            elapsedTime += Time.deltaTime;
            healthBar.value = Mathf.Lerp(currentHealth, targetHealth, elapsedTime / smoothTime);
            yield return null; // Wait for the next frame
        }

        // Ensure the final value is set precisely
        healthBar.value = targetHealth;
    }
}
