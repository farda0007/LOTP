using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public int count = 10;

    public GameObject Hero1;

    //private void Start()
    //{
    //    Instantiate(Hero1, SpawnPoints[0].transform.position, Quaternion.identity);
    //    StartCoroutine(SpawnCoroutine());

        //private IEnumerator SpawnCoroutine()
        //{
        //    while (count > 0)
        //    {
        //        //count--;

        //        //int randomSpawnPoint = Random.Range(0, SpawnPoints.Count);
        //        //var randomOffset = Random.insideUnitSphere;
        //        //var spawnPointPosition = SpawnPoints[randomSpawnPoint].transform.position + (Vector3)randomOffset;

        //        //SpawnHero(Hero1,spawnPointPosition);

        //        //yield return new WaitForSeconds(1f);
        //    }
        //}


}
