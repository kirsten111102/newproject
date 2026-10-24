using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

[RequireComponent(typeof(EnemyAI))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;
    private Rigidbody2D rb;
    //private Transform playerTransform;
    private EnemyAI enemyAI;
    private float knockbackEndTime;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
    }

    void FixedUpdate()
    {
        if (!enemyAI.IsChasing) return;
        if (Time.time < knockbackEndTime) return;
        Transform player = enemyAI.Player;
        if (player == null) return;

        Vector2 direction = ((Vector2)player.position - rb.position).normalized;

        Vector2 targetPosition = rb.position + moveSpeed * Time.fixedDeltaTime * direction;

        rb.MovePosition(targetPosition);
    }

    public void ApplyKnockbackDelay(float duration)
    {
        knockbackEndTime = Time.time + duration;
    }
}
