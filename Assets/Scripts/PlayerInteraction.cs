using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    /*[SerializeField]
    private int attackDmg = 10;*/
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            Debug.Log("You hit the " + collision.gameObject.name + "!");
        // else if (collision.gameObject.CompareTag("Enemy"))
        // {
        //     EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        //     if(enemyHealth != null)
        //         enemyHealth.TakeDamage(attackDmg);
        // }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Congrats, you get a coin!");
        }
    }
}
