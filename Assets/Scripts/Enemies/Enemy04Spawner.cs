using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04Spawner : MonoBehaviour
{
  [Header("Spawn Setup")]
  public int numOfEnemies;  // número de inimigos
  public float respawnTime; // tempo entre cada respawn

  [Header("Enemy Attributes")]
  public float enemySpeed;        // velocidade do inimigo
  public float enemyShootingRate; // quantos segundos entre cada tiro

  [Header("Prefabs")]
  public GameObject enemyGO;

  ObjectLifeData enemyLifeData;

  void Start()
  {
    enemyLifeData = ScriptableObject.CreateInstance<ObjectLifeData>();
    enemyLifeData.fullLife = 30;
    enemyLifeData.timeBetweenDamage = 0.5f;
    enemyLifeData.invulnerableOnDamage = false;
  }

  public void SpawnEnemy()
  {
    // Limite da tela
    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    float height = (topRight.y - bottomLeft.y) / numOfEnemies;
    for (int i = 0; i < numOfEnemies; i++)
    {
      GameObject enemy = (GameObject)Instantiate(enemyGO);
      enemy.GetComponent<Enemy04Control>().InitAttributes(enemySpeed, enemyShootingRate);
      enemy.GetComponent<LifeManager>().lifeData = enemyLifeData;

      Vector2 position = new Vector2(topRight.x, bottomLeft.y + height * i + height / 2);
      enemy.transform.position = position;
    }

    ScheduleNextSpawnEnemy();
  }

  private void ScheduleNextSpawnEnemy()
  {
    Invoke("SpawnEnemy", respawnTime);
  }

  public void UnscheduleNextSpawnEnemy()
  {
    CancelInvoke("SpawnEnemy");
  }
}
