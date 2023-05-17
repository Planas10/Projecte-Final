using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEnemyPos : MonoBehaviour
{
    private List<AI_Enemy> enemies; // Array de los enemigos actualmente en escena
    private List<Transform> enemyInitPos;

    private void Awake()
    {
        enemies = new(FindObjectsOfType<AI_Enemy>());
        enemies.Sort((a, b) => { return a.name.CompareTo(b.name); });
    }

    private void Start()
    {
        for (int i = 0; i < enemies.ToArray().Length; i++)
        {
            enemyInitPos[i] = enemies[i].GetComponent<AI_Enemy>().InitialPos;
        }
    }

    private void Update()
    {
        for (int i = 0; i < enemies.ToArray().Length; i++)
        {
            if (!enemies[i].GetComponent<AI_Enemy>().playerChased)
            {
                return;
            }
        }
        for (int i = 0; i < enemies.ToArray().Length; i++)
        {
            enemies[i].transform.position = enemyInitPos[i].position;
            enemies[i].GetComponent<AI_Enemy>().playerChased = false;
        }
        
    }
}

