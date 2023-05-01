using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCircleControl : MonoBehaviour
{
  float speed; // velocidade do tiro
  Vector2 _direction; // direção do tiro

  void Start()
  {
    speed = 10f;
  }

  public void SetDirection(Vector2 direction)
  {
    _direction = direction.normalized;
  }

  void Update()
  {
    // atualiza a posição do tiro
    Vector2 position = transform.position;
    position += _direction * speed * Time.deltaTime;
    transform.position = position;

    // remove o tiro da tela quando ele sai da área visível
    Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
        (transform.position.y < min.y) || (transform.position.y > max.y))
      Destroy(gameObject);
  }
}
