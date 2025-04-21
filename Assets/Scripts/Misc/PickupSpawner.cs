using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoinPrefab, healthGlobe;

    public void DropItems()
    {
        int randomNumber = Random.Range(1, 5); // returns 1 or 2

        if (randomNumber == 1)
        {
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }
        else if (randomNumber >= 2 && randomNumber <= 4)

        {
            Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
        }
    }

}
