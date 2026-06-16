using UnityEngine;
using UnityEngine.UI;

public class HealthSliderUI : MonoBehaviour
{
    public GlobalHealth playerHealth;
    public Slider slider;

    void Start()
    {
        slider.maxValue = playerHealth.MaxHealth;

        playerHealth.onHealthChanged += UpdateUI;
        UpdateUI();
    }

    void UpdateUI()
    {
        slider.value = playerHealth.CurrentHealth;
    }
}