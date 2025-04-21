using UnityEngine;

public class Shroom : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject shroomProjectilePrefab;

    private Animator myAnimator;
    private SpriteRenderer SpriteRenderer;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(ATTACK_HASH);

        if (transform.position.x - PlayerController.Instance.transform.position.x < 0)
        {
            SpriteRenderer.flipX = false; 
        }
        else
        {
            SpriteRenderer.flipX = true; 
        }
    }

    public void SpawnProjectileAnimEvent()
    {
        Instantiate(shroomProjectilePrefab, transform.position, Quaternion.identity);
    }
}
