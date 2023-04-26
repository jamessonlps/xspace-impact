using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy06Spawner : MonoBehaviour
{
  public GameObject enemyGO;

  public int numOfEnemies;
  public float spawnTime;
  public float respawnTime;

  void Start()
  {
    numOfEnemies = 3;
    spawnTime = 1f;
    respawnTime = 120f;
    Invoke("SpawnEnemy", spawnTime);
  }

  void Update()
  {

  }

  void SpawnEnemy()
  {
    // Limite da tela
    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    // divide altura da tela por pelo número de inimigos
    float height = (topRight.y - bottomLeft.y) / numOfEnemies;
    for (int i = 0; i < numOfEnemies; i++)
    {
      GameObject enemy = (GameObject)Instantiate(enemyGO);
      Vector2 position = new Vector2(topRight.x, bottomLeft.y + height * i + height / 2);
      enemy.transform.position = position;
    }

    ScheduleNextSpawn();
  }

  void ScheduleNextSpawn()
  {
    Invoke("SpawnEnemy", respawnTime);
  }
}
