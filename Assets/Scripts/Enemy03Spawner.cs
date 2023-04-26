using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03Spawner : MonoBehaviour
{
  public GameObject enemy03GO;

  void Start()
  {
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

    // divide altura da tela por 5 e cria 5 inimigos igualmente espaçados
    float height = (topRight.y - bottomLeft.y) / 5;
    for (int i = 0; i < 5; i++)
    {
      GameObject enemy = (GameObject)Instantiate(enemy03GO);
      Vector2 position = new Vector2(topRight.x, bottomLeft.y + height * i + height / 2);
      enemy.transform.position = position;
    }

    ScheduleNextSpawn();
  }

  void ScheduleNextSpawn()
  {
    Invoke("SpawnEnemy", 30f);
  }
}
