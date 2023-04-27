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
  public int enemyHealth;         // vida do inimigo

  [Header("Prefabs")]
  public GameObject enemyGO;

  void Start()
  {
    Invoke("SpawnEnemy", 1f);
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
      enemy.GetComponent<Enemy04Control>().InitAttributes(enemySpeed, enemyShootingRate, enemyHealth);
      Vector2 position = new Vector2(topRight.x, bottomLeft.y + height * i + height / 2);
      enemy.transform.position = position;
    }

    Invoke("SpawnEnemy", respawnTime);
  }
}
