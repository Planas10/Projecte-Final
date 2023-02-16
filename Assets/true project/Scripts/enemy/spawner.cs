using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
   
    public GameObject enemy;
    public float spawnInterval;
    private int enemiesSpawned;
    private int maxEnemies = 50;

    [SerializeField]
    private float x_lenght;
    [SerializeField]
    private float z_lenght;

    void Start()
    {
        
        StartCoroutine(SpawnEnemy(spawnInterval, enemy));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy) 
    {
        if (enemiesSpawned < maxEnemies)
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(x_lenght/2, -(x_lenght/2)), 0.5f, Random.Range(z_lenght/2, -(z_lenght/2))), Quaternion.identity);
            enemiesSpawned++;
            StartCoroutine(SpawnEnemy(interval, enemy));
        }
        else
        {
            yield return new WaitForSeconds(interval);
            StartCoroutine(SpawnEnemy(interval, enemy));
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(transform.position, new Vector3(x_lenght, 1, z_lenght));
    //}

}
