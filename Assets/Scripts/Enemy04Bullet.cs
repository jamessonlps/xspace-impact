using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04Bullet : MonoBehaviour
{
  public float speed;

  // Start is called before the first frame update
  void Start()
  {
    speed = 20f;
  }

  // Update is called once per frame
  void Update()
  {
    Vector2 position = transform.position;
    position = new Vector2(position.x - speed * Time.deltaTime, position.y);
    transform.position = position;

    Vector2 leftBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 rightTop = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    if (transform.position.x < leftBottom.x || transform.position.x > rightTop.x ||
        transform.position.y < leftBottom.y || transform.position.y > rightTop.y)
      Destroy(gameObject);
  }
}
