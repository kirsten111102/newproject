using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;
    [SerializeField]
    private HealthBarUI healthBarUI;
    [SerializeField]
    private GameManager gameManager;
    // private void Awake()
    // {
    //     currentHealth = maxHealth;
    // }

    private void Start()
    {
        if (GameSession.Instance != null &&
        GameSession.Instance.HasSavedHealth)
        {
            currentHealth = Mathf.Clamp(
            GameSession.Instance.CurrentHealth,
            0,
            maxHealth
            );
        }
        else
        {
            currentHealth = maxHealth;
            SaveHealth();
        }

        if (healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
            healthBarUI.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || currentHealth == 0) return;

        currentHealth = Mathf.Max(currentHealth - damage, 0);

        //Debug.Log("Player Health: " + currentHealth);
        if (healthBarUI != null)
            healthBarUI.SetHealth(currentHealth);

        UpdateHealth();
        if (currentHealth == 0) Die();
    }
    private void Die()
    {
        Debug.Log("Player died");

        if (gameManager != null)
            gameManager.GameOver();

        gameObject.SetActive(false);
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || currentHealth == 0) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        if (healthBarUI != null) healthBarUI.SetHealth(currentHealth);

        UpdateHealth();
    }

    private void UpdateHealth()
    {
        SaveHealth();

        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }
    }
    private void SaveHealth()
    {
        if (GameSession.Instance != null)
        {
            GameSession.Instance.SaveHealth(currentHealth);
        }
    }
}
