using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private SpriteRenderer spriteRenderer;
    private Vector2 lastPosition;


    private bool canAttack = true;

    private enum State
    {
        Roaming,
        Attacking
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private Vector2 roamPosition;
    private float timeRoaming = 0;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        state = State.Roaming;
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
        lastPosition = transform.position;

    }

    private void Update()
    {
        MovementStateControl();
        FlipSprite();
        lastPosition = transform.position;
    }

    private void FlipSprite()
    {
        Vector2 moveDir = (Vector2)transform.position - lastPosition;

        if (moveDir.x > 0.01f)
            spriteRenderer.flipX = false; // Facing right
        else if (moveDir.x < -0.01f)
            spriteRenderer.flipX = true;  // Facing left
    }


    private void MovementStateControl()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
            break;

            case State.Attacking:
                Attacking();
            break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enemyPathfinding.MoveTo(roamPosition);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }

        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();

            if (stopMovingWhileAttacking)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    //private IEnumerator RoamingRoutine()
    //{
    //    while (state == State.Roaming)
    //    {
    //        Vector2 roamPosition = GetRoamingPosition();
    //        enemyPathfinding.MoveTo(roamPosition);
    //        yield return new WaitForSeconds(2f);
    //    }
    //}


    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
