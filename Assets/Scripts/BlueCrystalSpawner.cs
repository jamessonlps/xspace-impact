using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCrystalSpawner : MonoBehaviour
{
    [Header("Spawn Control")]
    public float spawnRate;      // intervalo de tempo entre os spawns
    public float minSpawnRate;   // intervalo mínimo de tempo entre os spawns
    public float spawnRateDelta; // variação do intervalo de tempo entre os spawns

    [Header("Prefabs")]
    public GameObject[] BlueCrystal;

    // Start is called before the first frame update
    void Start()
    {
    SpawnCrystalBlue(); // TODO: remover do start
    }

    void SpawnCrystalBlue()
    {
        // Limite da tela
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Escolhe um BlueCrystal aleatório
        int randomIndex = Random.Range(0, BlueCrystal.Length);
        GameObject bluecrystal = (GameObject)Instantiate(BlueCrystal[randomIndex]);

        if (randomIndex == 0)
        bluecrystal.GetComponent<BlueCrystalControl>().InitAttributes(3f);
        else
        bluecrystal.GetComponent<BlueCrystalControl>().InitAttributes(5f);

        // Posiciona o BlueCrystal
        Vector2 offset = bluecrystal.GetComponent<Renderer>().bounds.size;
        Vector2 position = new Vector2(topRight.x + offset.x, Random.Range(bottomLeft.y + offset.y, topRight.y - offset.y));
        bluecrystal.transform.position = position;

        // Chama a função novamente
        ScheduleNextBlueCrystalSpawn();
    }

    void ScheduleNextBlueCrystalSpawn()
    {
        if (spawnRate > minSpawnRate)
        Invoke("UpdateBlueCrystalSpawnRate", 1f);

        Invoke("SpawnCrystalBlue", spawnRate);
    }

    void UpdateBlueCrystalSpawnRate()
    {
        spawnRate -= spawnRateDelta;
    }
}
