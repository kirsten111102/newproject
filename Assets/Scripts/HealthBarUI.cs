using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private TextMeshProUGUI healthText;
    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        UpdateHealthText(maxHealth, maxHealth);
    }

    public void SetHealth(int currentHealth)
    {
        healthSlider.value = currentHealth;

        UpdateHealthText(currentHealth, (int)healthSlider.maxValue);
    }

    private void UpdateHealthText(int current, int max)
    {
        if (healthText != null)
        {
            healthText.text = $"{current}/{max}";
        }
    }
}
