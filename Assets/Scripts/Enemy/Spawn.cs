using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public GameObject mobSpawn;
    public int count = 10;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void SpawnMob(GameObject mob, Vector3 spawnpoint) 
    { 
        Instantiate(mob, spawnpoint, Quaternion.identity); 
    }

    IEnumerator SpawnRoutine()
    {
        while (count > 0)
        {
            count--;
            int randomSpawnPoint = Random.Range(0, SpawnPoints.Count);
            var randomOffset = Random.insideUnitSphere;
            var spawnPointPosition = SpawnPoints[randomSpawnPoint].transform.position + (Vector3)randomOffset;

            SpawnMob(mobSpawn, spawnPointPosition);

            yield return new WaitForSeconds(1f);
        }
    }

}
