using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Spawner : MonoBehaviour
{
  float spawnEnemy01Rate = 2f;

  public GameObject[] enemiesGO;


  void Start()
  {
    SpawnEnemy(); // TODO: remover do start
  }

  void Update()
  {

  }

  void SpawnEnemy()
  {
    // Limite da tela
    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    // Escolhe um inimigo aleatório
    int randomIndex = Random.Range(0, enemiesGO.Length);
    GameObject enemy = (GameObject)Instantiate(enemiesGO[randomIndex]);

    // Posiciona o inimigo
    Vector2 position = new Vector2(topRight.x + 5f, Random.Range(bottomLeft.y, topRight.y));
    enemy.transform.position = position;

    // Chama a função novamente
    ScheduleNextEnemySpawn();
  }

  void ScheduleNextEnemySpawn()
  {
    if (spawnEnemy01Rate > 0.5f)
      spawnEnemy01Rate -= 0.05f;

    Invoke("SpawnEnemy", spawnEnemy01Rate);
  }
}
