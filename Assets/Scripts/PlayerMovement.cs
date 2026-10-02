using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;

    private Rigidbody2D rb;
    private Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new(horizontal, vertical);

        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }
    }

    void FixedUpdate()
    {
        Vector2 targetposition = rb.position + (direction * moveSpeed * Time.fixedDeltaTime);

        rb.MovePosition(targetposition);
    }
}
