using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    private enum PickupType
    {
        GoldCoin,
        HeartGlobe
    }

    [SerializeField] private PickupType pickupType;
    [SerializeField] private float pickUpDistance = 5;
    [SerializeField] private float accelerationRate = .2f;
    [SerializeField] private float pickUpSpeed = 3;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration;


    private Vector3 moveDir;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine()); // starter animCurve spawn rutinen
    }

    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position; // henter players position

        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance) // hvis spilleren er innenfor pickUpDistance
        {
            moveDir = (playerPos - transform.position).normalized; // henter retningen fra spilleren til pickupen
            pickUpSpeed += accelerationRate;
        }
        else
        {
            moveDir = Vector3.zero;
            pickUpSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDir * (pickUpSpeed * Time.deltaTime); // flytter pickupen mot spilleren
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            DetectPickupType(); // sjekker hvilken pickup type det er
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;  // Calculate the linear interpolation factor 
            float heightT = animCurve.Evaluate(linearT); // Evaluate the animation curve at the current time
            float height = Mathf.Lerp(0f, heightY, heightT); // Lerp between 0 and 1 based on the curve

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height); // Lerp the position of the object
            yield return null;
        }
    }

    private void DetectPickupType()
    {
        switch (pickupType)
        {
            case PickupType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold(); 
                break;
            case PickupType.HeartGlobe:
                PlayerHealth.Instance.HealPlayer();
                break;
        }
    }
}
