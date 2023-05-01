using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCrystalSpawner : MonoBehaviour
{
    [Header("Spawn Control")]
    public float spawnRate;      // intervalo de tempo entre os spawns
    public float minSpawnRate;   // intervalo mínimo de tempo entre os spawns
    public float spawnRateDelta; // variação do intervalo de tempo entre os spawns

    [Header("Prefabs")]
    public GameObject[] GreenCrystal;

    // Start is called before the first frame update
    void Start()
    {
    SpawnCrystalGreen(); // TODO: remover do start
    }

    void SpawnCrystalGreen()
    {
        // Limite da tela
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Escolhe um GreenCrystal aleatório
        int randomIndex = Random.Range(0, GreenCrystal.Length);
        GameObject greencrystal = (GameObject)Instantiate(GreenCrystal[randomIndex]);

        if (randomIndex == 0)
        greencrystal.GetComponent<GreenCrystalControl>().InitAttributes(3f);
        else
        greencrystal.GetComponent<GreenCrystalControl>().InitAttributes(5f);

        // Posiciona o GreenCrystal
        Vector2 offset = greencrystal.GetComponent<Renderer>().bounds.size;
        Vector2 position = new Vector2(topRight.x + offset.x, Random.Range(bottomLeft.y + offset.y, topRight.y - offset.y));
        greencrystal.transform.position = position;

        // Chama a função novamente
        ScheduleNextGreenCrystalSpawn();
    }

    void ScheduleNextGreenCrystalSpawn()
    {
        if (spawnRate > minSpawnRate)
        Invoke("UpdateGreenCrystalSpawnRate", 1f);

        Invoke("SpawnCrystalGreen", spawnRate);
    }

    void UpdateGreenCrystalSpawnRate()
    {
        spawnRate -= spawnRateDelta;
    }
}
