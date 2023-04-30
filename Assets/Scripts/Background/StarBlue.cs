using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBlue : MonoBehaviour
{
  public float speed;

  void Update()
  {
    Vector2 position = transform.position;
    position.x -= speed * Time.deltaTime;
    transform.position = position;

    Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    if (transform.position.x < min.x)
      transform.position = new Vector2(max.x, Random.Range(min.y, max.y));
  }
}
