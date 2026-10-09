using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private int damage = 10;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null) playerHealth.TakeDamage(damage);
        }
    }
}
