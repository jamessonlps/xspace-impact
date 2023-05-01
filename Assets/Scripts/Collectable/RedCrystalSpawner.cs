using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCrystalSpawner : MonoBehaviour
{
  [Header("Spawn Control")]
  public float spawnRate;      // intervalo de tempo entre os spawns
  public float minSpawnRate;   // intervalo mínimo de tempo entre os spawns
  public float spawnRateDelta; // variação do intervalo de tempo entre os spawns

  [Header("Prefabs")]
  public GameObject[] RedCrystal;

  public void SpawnCrystalRed()
  {
    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    int randomIndex = Random.Range(0, RedCrystal.Length);
    GameObject redcrystal = (GameObject)Instantiate(RedCrystal[randomIndex]);

    Vector2 offset = redcrystal.GetComponent<Renderer>().bounds.size;
    Vector2 position = new Vector2(topRight.x + offset.x, Random.Range(bottomLeft.y + offset.y, topRight.y - offset.y));
    redcrystal.transform.position = position;

    ScheduleNextRedCrystalSpawn();
  }

  private void ScheduleNextRedCrystalSpawn()
  {
    if (spawnRate > minSpawnRate)
      Invoke("UpdateRedCrystalSpawnRate", 1f);

    Invoke("SpawnCrystalRed", spawnRate);
  }

  public void UnscheduleNextRedCrystalSpawn()
  {
    CancelInvoke("SpawnCrystalRed");
    CancelInvoke("UpdateRedCrystalSpawnRate");
  }

  private void UpdateRedCrystalSpawnRate()
  {
    spawnRate -= spawnRateDelta;
  }
}
