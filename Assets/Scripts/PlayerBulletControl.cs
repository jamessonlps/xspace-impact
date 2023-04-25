using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletControl : MonoBehaviour
{
  public float speed;
  void Start()
  {
    speed = 20f;
  }

  void Update()
  {
    // posição atual do bullet
    Vector2 position = transform.position;

    // calcula a nova posição
    position = new Vector2(position.x + speed * Time.deltaTime, position.y);

    // atualiza a posição do bullet
    transform.position = position;

    // canto superior direito da tela
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    // se o bullet sair da tela, destrói o objeto
    if (transform.position.x > topRight.x)
    {
      Destroy(gameObject);
    }

  }
}
