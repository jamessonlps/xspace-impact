using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Control : MonoBehaviour
{
  float speed;
  float health;

  public GameObject explosionAnimGO;

  // Start is called before the first frame update
  void Start()
  {
    speed = 5f;
    health = 100f;
  }

  // Update is called once per frame
  void Update()
  {
    Vector2 position = transform.position;

    // Atualiza a posição do inimigo
    position = new Vector2(position.x - speed * Time.deltaTime, position.y);
    transform.position = position;

    // Remove o inimigo quando ele sai da tela
    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    if (position.x < bottomLeft.x)
      Destroy(gameObject);
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    // Detecta colisão com o tiro
    if (collider.tag == "PlayerBulletTag")
    {
      health -= 50f;
      if (health <= 0)
      {
        PlayExplosionAnim();
        Destroy(gameObject);
      }
    }
  }

  void PlayExplosionAnim()
  {

    // Cria a animação de explosão (auto-destrói após a execução)
    GameObject explosionAnim = (GameObject)Instantiate(explosionAnimGO);
    explosionAnim.transform.position = transform.position;
  }
}
