using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; // Reference to the UI slider

    // Set the max value of the slider and initialize health
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Update the health bar value
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
