using System.Collections;
using UnityEngine;

public class ShroomProjectile : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject shroomProjectileShadow;
    [SerializeField] private GameObject splatterPrefab;

    private void Start()
    {
        GameObject shroomShadow = Instantiate(shroomProjectileShadow, transform.position + new Vector3(0, -0.3f, 0), Quaternion.identity);

        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 shroomShadowStartPosition = shroomShadow.transform.position;

        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
        StartCoroutine(MoveGrapeShadowRoutine(shroomShadow, shroomShadowStartPosition, playerPos));
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;  
            float heightT = animCurve.Evaluate(linearT); // Evaluate the animation curve at the current time
            float height = Mathf.Lerp(0f, heightY, heightT); // Lerp between 0 and 1 based on the curve

            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

            yield return null;
        }

        Instantiate(splatterPrefab, transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }

    private IEnumerator MoveGrapeShadowRoutine(GameObject shroomShadow, Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            shroomShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);
            yield return null;
        }
        Destroy(shroomShadow);
    }
}
