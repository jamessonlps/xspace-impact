using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetControl : MonoBehaviour
{
  public GameObject[] planets;

  private Queue<GameObject> planetQueue;
  private GameObject currPlanet;

  void Start()
  {
    planetQueue = new Queue<GameObject>(planets);
    InstantiatePlanet();
  }

  void InstantiatePlanet()
  {
    // Planeta é criado à direita da tela, levando em conta seu tamanho
    // O valor da posição em y é aleatória também levando em conta seu tamanho
    GameObject planet = planetQueue.Dequeue();

    Vector2 planetSize = planet.GetComponent<SpriteRenderer>().bounds.size;
    Vector2 rightTopCameraBorder = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    Vector2 leftBottomCameraBorder = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 position = new Vector2(
        rightTopCameraBorder.x + planetSize.x,
        Random.Range(leftBottomCameraBorder.y / 2, rightTopCameraBorder.y / 2)
    );

    currPlanet = Instantiate(planet, position, Quaternion.identity);
    float randomScale = Random.Range(1f, 2.5f);
    currPlanet.transform.localScale = new Vector2(randomScale, randomScale);
    planetQueue.Enqueue(planet);
  }

  void Update()
  {
    if (currPlanet == null)
      InstantiatePlanet();
  }
}
