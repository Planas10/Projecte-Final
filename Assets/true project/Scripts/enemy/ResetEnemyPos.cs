using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEnemyPos : MonoBehaviour
{
    private List<AI_Enemy> enemies; // Array de los enemigos actualmente en escena

    private void Awake()
    {
        enemies = new(FindObjectsOfType<AI_Enemy>());
        enemies.Sort((a, b) => { return a.name.CompareTo(b.name); });
    }

    private void Update()
    {
        bool playerchased = false;

        for (int i = 0; i < enemies.ToArray().Length; i++)
        {
            if (enemies[i].GetComponent<AI_Enemy>().playerChased)
            {
                playerchased = true;
            }
        }
        if (playerchased)
        {
            for (int i = 0; i < enemies.ToArray().Length; i++)
            {
                if (!enemies[i].Trapped)
                {
                    enemies[i].transform.position = enemies[i].InitialPos;
                }
                enemies[i].GetComponent<AI_Enemy>().playerChased = false;
            }
        }

    }
}

