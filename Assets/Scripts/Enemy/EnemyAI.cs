using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 1f;   // Close range for melee attack
    [SerializeField] private float chaseRange = 5f;    // Start chasing from this distance
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = true;

    private bool canAttack = true;
    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private EnemyPathfinding enemyPathfinding;
    private enum State { Roaming, Chasing, Attacking }
    private State state;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);

        switch (state)
        {
            case State.Roaming:
                if (distanceToPlayer < chaseRange)
                {
                    state = State.Chasing;
                }
                else
                {
                    Roaming();
                }
                break;

            case State.Chasing:
                if (distanceToPlayer < attackRange)
                {
                    state = State.Attacking;
                }
                else if (distanceToPlayer > chaseRange)
                {
                    state = State.Roaming;
                    roamPosition = GetRoamingPosition();
                }
                else
                {
                    enemyPathfinding.MoveTo(
                        (PlayerController.Instance.transform.position - transform.position).normalized
                    );
                }
                break;

            case State.Attacking:
                if (distanceToPlayer > attackRange)
                {
                    state = State.Chasing;
                }
                else
                {
                    if (canAttack)
                    {
                        canAttack = false;
                        (enemyType as IEnemy).Attack();

                        if (stopMovingWhileAttacking)
                            enemyPathfinding.StopMoving();
                        else
                            enemyPathfinding.MoveTo(
                                (PlayerController.Instance.transform.position - transform.position).normalized
                            );

                        StartCoroutine(AttackCooldownRoutine());
                    }
                }
                break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
        enemyPathfinding.MoveTo(roamPosition);

        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
            timeRoaming = 0f;
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
