using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float minX, minZ, maxX, maxZ;
    public GameObject enemyPrefab;
    float randomX, randomZ;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        randomX = Random.Range(minX, maxX);
        randomZ = Random.Range(minZ, maxZ);
        Instantiate(enemyPrefab, new Vector3(randomX, 0, randomZ), Quaternion.identity);
        yield return new WaitForSeconds(4.0f);
        StartCoroutine(SpawnEnemy());
    }
}
