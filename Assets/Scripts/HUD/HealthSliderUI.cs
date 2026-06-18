using UnityEngine;
using UnityEngine.UI;

public class HealthSliderUI : MonoBehaviour
{
    public GlobalHealth targetHealth;
    public Slider slider;

    void Start()
    {
        slider.maxValue = targetHealth.MaxHealth;

        targetHealth.onHealthChanged += UpdateUI;
        UpdateUI();
    }

    void UpdateUI()
    {
        slider.value = targetHealth.CurrentHealth;

        if (targetHealth.CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}