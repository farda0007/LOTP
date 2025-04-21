using Unity.Cinemachine;
using UnityEngine;

public class ShroomLandSplatter : MonoBehaviour
{
    private SpriteFade spriteFade;

    private void Awake()
    {
        spriteFade = GetComponent<SpriteFade>();
    }

    private void Start()
    {
        StartCoroutine(spriteFade.SlowFadeRoutine());

        Invoke("DisableCollider", 0.2f); // Disable the collider after 0.2 seconds
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        playerHealth?.TakeDamage(3, transform);
    }

    private void DisableCollider()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
