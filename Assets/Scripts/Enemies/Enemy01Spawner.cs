using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Spawner : MonoBehaviour
{
  [Header("Spawn Control")]
  public float spawnRate;      // intervalo de tempo entre os spawns
  public float minSpawnRate;   // intervalo mínimo de tempo entre os spawns
  public float spawnRateDelta; // variação do intervalo de tempo entre os spawns

  [Header("Prefabs")]
  public GameObject[] enemiesGO;

  ObjectLifeData enemyLifeData0;
  ObjectLifeData enemyLifeData1;

  void Start()
  {
    enemyLifeData0 = ScriptableObject.CreateInstance<ObjectLifeData>();
    enemyLifeData1 = ScriptableObject.CreateInstance<ObjectLifeData>();

    enemyLifeData0.fullLife = 2;
    enemyLifeData0.timeBetweenDamage = 0.5f;
    enemyLifeData0.invulnerableOnDamage = false;

    enemyLifeData1.fullLife = 1;
    enemyLifeData1.timeBetweenDamage = 0.5f;
    enemyLifeData1.invulnerableOnDamage = false;

    SpawnEnemy(); // TODO: remover do start
  }

  void Update()
  {

  }

  void SpawnEnemy()
  {
    // Escolhe um inimigo aleatório
    int randomIndex = Random.Range(0, enemiesGO.Length);
    GameObject enemy = (GameObject)Instantiate(enemiesGO[randomIndex]);

    if (randomIndex == 0)
    {
      enemy.GetComponent<Enemy01Control>().InitAttributes(3f);
      enemy.GetComponent<LifeManager>().lifeData = enemyLifeData0;
    }
    else
    {
      enemy.GetComponent<Enemy01Control>().InitAttributes(5f);
      enemy.GetComponent<LifeManager>().lifeData = enemyLifeData1;
    }

    // Posiciona o inimigo
    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    Vector2 offset = enemy.GetComponent<Renderer>().bounds.size;
    Vector2 position = new Vector2(topRight.x + offset.x, Random.Range(bottomLeft.y + offset.y, topRight.y - offset.y));
    enemy.transform.position = position;

    // Chama a função novamente
    ScheduleNextEnemySpawn();
  }

  void ScheduleNextEnemySpawn()
  {
    if (spawnRate > minSpawnRate)
      Invoke("UpdateEnemySpawnRate", 1f);
    Invoke("SpawnEnemy", spawnRate);
  }

  void UpdateEnemySpawnRate()
  {
    spawnRate -= spawnRateDelta;
  }
}
