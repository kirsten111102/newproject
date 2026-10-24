
using UnityEngine;

public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    Patrol
}

public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float chaseRange = 6f;
    [SerializeField] private float attackRange = 1.5f;

    [Header("Patrol")]
    [SerializeField] private float patrolDistance = 2f;
    [SerializeField] private float patrolSpeed = 1.5f;
    [SerializeField] private float patrolWaitTime = 1f;
    [SerializeField] private float homeThreshold = 0.1f;

    private Vector3 homePosition;
    private Vector3 patrolTarget;
    private bool movingRight = true;
    private bool returningHome;
    private float patrolWaitTimer;

    private EnemyState currentState = EnemyState.Idle;
    private EnemyCombat enemyCombat;

    public bool IsChasing => currentState == EnemyState.Chase;
    public Transform Player => player;
    public EnemyState CurrentState => currentState;

    private void Awake()
    {
        enemyCombat = GetComponent<EnemyCombat>();

        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        if (enemyCombat == null)
        {
            Debug.LogError(
                gameObject.name + " is missing EnemyCombat!",
                this
            );
        }
        homePosition = transform.position;
        patrolTarget = homePosition + Vector3.right * patrolDistance;
    }

    private void Update()
    {
        if (player == null)
        {
            ChangeState(EnemyState.Idle);
            return;
        }

        UpdateState();

        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;

            case EnemyState.Chase:
                HandleChase();
                break;

            case EnemyState.Attack:
                HandleAttack();
                break;

            case EnemyState.Patrol:
                HandlePatrol();
                break;
        }
    }

    private float GetDistanceToPlayer()
    {
        return Vector2.Distance(
            transform.position,
            player.position
        );
    }

    private void UpdateState()
    {
        float distance = GetDistanceToPlayer();

        if (player != null && distance <= chaseRange)
        {
            returningHome = false;

            if (distance <= attackRange)
                ChangeState(EnemyState.Attack);
            else
                ChangeState(EnemyState.Chase);

            return;
        }

        if (currentState == EnemyState.Chase ||
            currentState == EnemyState.Attack)
        {
            returningHome = true;
        }

        if (returningHome)
        {
            if (Vector2.Distance(transform.position, homePosition)
                > homeThreshold)
            {
                ChangeState(EnemyState.Patrol);
                return;
            }

            returningHome = false;
            movingRight = true;
            patrolTarget = homePosition
                + Vector3.right * patrolDistance;
            patrolWaitTimer = patrolWaitTime;
        }

        ChangeState(EnemyState.Patrol);
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;

        Debug.Log(
            gameObject.name + " state: " + currentState
        );
    }

    private void HandleIdle()
    {
        // The next UpdateState() call handles detection.
    }

    private void HandleChase()
    {
        MoveTowardPlayer();
    }

    private void HandlePatrol()
    {
        // Return to the original position first.
        if (returningHome)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                homePosition,
                moveSpeed * Time.deltaTime
            );

            return;
        }

        // Pause briefly at each patrol endpoint.
        if (patrolWaitTimer > 0f)
        {
            patrolWaitTimer -= Time.deltaTime;
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            patrolTarget,
            patrolSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, patrolTarget) <= 0.05f)
        {
            movingRight = !movingRight;

            patrolTarget = homePosition
                + (movingRight ? Vector3.right : Vector3.left)
                * patrolDistance;

            patrolWaitTimer = patrolWaitTime;
        }
    }

    private void MoveTowardPlayer()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            moveSpeed * Time.deltaTime
        );
    }

    private void HandleAttack()
    {
        if (enemyCombat == null)
            return;

        enemyCombat.TryAttack(player);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(
        transform.position,
        Vector3.forward,
        chaseRange
        );

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(
        transform.position,
        Vector3.forward,
        attackRange
        );
    }
#endif
}
