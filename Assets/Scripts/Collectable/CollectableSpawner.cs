using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
  [Header("Spawn Control")]
  [SerializeField] private float spawnRate = 60f;      // intervalo de tempo entre os spawns
  [SerializeField] private float minSpawnRate = 45f;   // intervalo mínimo de tempo entre os spawns
  [SerializeField] private float spawnRateDelta = 0.5f; // variação do intervalo de tempo entre os spawns

  [Header("Prefabs")]
  [SerializeField] private GameObject lifeCollectable;

  public void SpawnLifeCollectable()
  {
    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    GameObject life = Instantiate(lifeCollectable);
    Vector2 offset = life.GetComponent<Renderer>().bounds.size;
    Vector2 position = new Vector2(topRight.x + offset.x, Random.Range(bottomLeft.y + offset.y, topRight.y - offset.y));
    life.transform.position = position;

    ScheduleNextLifeCollectableSpawn();
  }

  public void ScheduleNextLifeCollectableSpawn()
  {
    if (spawnRate > minSpawnRate)
      Invoke("UpdateLifeCollectableSpawnRate", 45f);
    Invoke("SpawnLifeCollectable", spawnRate);
  }

  public void UnscheduleLifeCollectableSpawn()
  {
    CancelInvoke("SpawnLifeCollectable");
    CancelInvoke("UpdateLifeCollectableSpawnRate");
  }

  public void UpdateLifeCollectableSpawnRate()
  {
    spawnRate -= spawnRateDelta;
  }
}
