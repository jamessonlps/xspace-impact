using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Spawner : MonoBehaviour
{
  float spawnEnemy01Rate = 2f;
  float spawnEnemy01Red = 3f;

  public GameObject Enemy01BlueGO;
  public GameObject Enemy01RedGO;


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

    // Cria o inimigo
    GameObject enemy01Blue = (GameObject)Instantiate(Enemy01BlueGO);

    // Posiciona o inimigo
    Vector2 position = new Vector2(topRight.x + 5f, Random.Range(bottomLeft.y, topRight.y));
    enemy01Blue.transform.position = position;

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
