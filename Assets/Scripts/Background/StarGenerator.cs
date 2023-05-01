using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
  [SerializeField] private GameObject starBlueGO;
  [SerializeField] private GameObject starYellowGO;

  public int maxStars;

  void Start()
  {
    Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    for (int i = 0; i < maxStars; i++)
    {
      GameObject star = (GameObject)Instantiate(starBlueGO);

      star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
      star.GetComponent<StarBlue>().speed = (1f * Random.value + 0.5f);

      star.transform.parent = transform;
    }

    GameObject starYellow = (GameObject)Instantiate(starYellowGO);

    starYellow.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    starYellow.GetComponent<StarBlue>().speed = 0.1f;

    starYellow.transform.parent = transform;
  }
}
