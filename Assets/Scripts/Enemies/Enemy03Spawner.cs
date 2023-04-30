using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03Spawner : MonoBehaviour
{
  [Header("Control Variables")]
  public float SpawnerRate;  // quantos segundos entre cada spawn
  public int numOfEnemies;   // número de inimigos a serem criados
  public int numOfShoots;    // número de tiros que cada inimigo deve dar antes de girar
  public float enemySpeed;   // velocidade dos inimigos
  public float shootingRate; // quantos segundos entre cada tiro
  public int enemyHealth;    // saúde dos inimigos

  [Header("Prefabs")]
  public GameObject enemy03GO;

  ObjectLifeData enemyLifeData;

  void Start()
  {
    // TODO: Gerenciador de spawners atribui as varáveis de controle

    enemyLifeData = ScriptableObject.CreateInstance<ObjectLifeData>();
    enemyLifeData.fullLife = 10;
    enemyLifeData.timeBetweenDamage = 0.5f;
    enemyLifeData.invulnerableOnDamage = false;
    Invoke("SpawnEnemy", 1f);
  }

  void Update()
  {

  }

  void SpawnEnemy()
  {
    // Limite da tela
    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    // Cria os inimigos igualmente espaçados
    float height = (topRight.y - bottomLeft.y) / numOfEnemies;
    for (int i = 0; i < numOfEnemies; i++)
    {
      GameObject enemy = (GameObject)Instantiate(enemy03GO);

      enemy.GetComponent<LifeManager>().lifeData = enemyLifeData;
      enemy.GetComponent<Enemy03Control>().InitAttributes(
        enemySpeed,
        numOfShoots,
        shootingRate
      );

      Vector2 position = new Vector2(topRight.x, bottomLeft.y + height * i + height / 2);
      enemy.transform.position = position;
    }

    ScheduleNextSpawn();
  }

  void ScheduleNextSpawn()
  {
    Invoke("SpawnEnemy", SpawnerRate);
  }
}
