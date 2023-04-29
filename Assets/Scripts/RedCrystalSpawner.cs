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

    // Start is called before the first frame update
    void Start()
    {
    SpawnCrystalRed(); // TODO: remover do start
    }

    void SpawnCrystalRed()
    {
        // Limite da tela
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Escolhe um RedCrystal aleatório
        int randomIndex = Random.Range(0, RedCrystal.Length);
        GameObject redcrystal = (GameObject)Instantiate(RedCrystal[randomIndex]);

        if (randomIndex == 0)
        redcrystal.GetComponent<RedCrystalControl>().InitAttributes(3f);
        else
        redcrystal.GetComponent<RedCrystalControl>().InitAttributes(5f);

        // Posiciona o RedCrystal
        Vector2 offset = redcrystal.GetComponent<Renderer>().bounds.size;
        Vector2 position = new Vector2(topRight.x + offset.x, Random.Range(bottomLeft.y + offset.y, topRight.y - offset.y));
        redcrystal.transform.position = position;

        // Chama a função novamente
        ScheduleNextRedCrystalSpawn();
    }

    void ScheduleNextRedCrystalSpawn()
    {
        if (spawnRate > minSpawnRate)
        Invoke("UpdateRedCrystalSpawnRate", 1f);

        Invoke("SpawnCrystalRed", spawnRate);
    }

    void UpdateRedCrystalSpawnRate()
    {
        spawnRate -= spawnRateDelta;
    }
}
