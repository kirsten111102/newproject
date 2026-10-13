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
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        if (healthBarUI != null)
            healthBarUI.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        currentHealth = Mathf.Max(currentHealth - damage, 0);

        //Debug.Log("Player Health: " + currentHealth);
        if (healthBarUI != null)
            healthBarUI.SetHealth(currentHealth);

        if (currentHealth == 0) Die();
    }
    private void Die()
    {
        Debug.Log("Player died");

        if (gameManager != null)
            gameManager.GameOver();

        gameObject.SetActive(false);
    }
}
