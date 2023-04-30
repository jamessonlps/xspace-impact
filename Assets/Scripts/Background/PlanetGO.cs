using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGO : MonoBehaviour
{
  float speed = 0.5f;

  void Update()
  {
    Vector2 position = transform.position;
    position.x -= speed * Time.deltaTime;
    transform.position = position;

    Vector2 leftBottomCameraBorder = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 planetSize = GetComponent<SpriteRenderer>().bounds.size;

    if (transform.position.x + planetSize.x < leftBottomCameraBorder.x)
      Destroy(gameObject);
  }
}
