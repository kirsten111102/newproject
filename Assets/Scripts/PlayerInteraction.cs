using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    /*[SerializeField]
    private int attackDmg = 10;*/
    private PlayerInventory playerInventory;
    [SerializeField]
    private GameScore gameScore;

    private void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

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
        if (playerInventory == null) return;

        if (collision.CompareTag("Coin"))
        {
            if (gameScore == null) return;
            string coinName = collision.gameObject.name.Trim();
            var points = coinName switch
            {
                "BronzeCoin" => 1,
                "SilverCoin" => 5,
                "GoldenCoin" => 10,
                _ => 1,
            };

            playerInventory.AddCoin(1);
            gameScore.AddScore(points);
            Destroy(collision.gameObject);
            return;
        }

        ItemPickup itemPickup = collision.GetComponent<ItemPickup>();
        if(itemPickup != null)
        {
            Item item = itemPickup.CreateItem();
            playerInventory.AddItem(item);
            Debug.Log("Picked up: " + item.ItemName);
            Destroy(collision.gameObject);
        }
    }
}
