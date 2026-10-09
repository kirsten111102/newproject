using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Transform playerTransform;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }
    void FixedUpdate()
    {
        if (playerTransform == null) return;

        Vector2 direction = ((Vector2)playerTransform.position - rb.position).normalized;

        Vector2 targetPosition = rb.position + moveSpeed * Time.fixedDeltaTime * direction;

        rb.MovePosition(targetPosition);
    }
}
