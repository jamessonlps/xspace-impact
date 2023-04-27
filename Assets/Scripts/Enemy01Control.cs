using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Control : MonoBehaviour
{
  [Header("Attributes")]
  public float speed;
  public float health;

  [Header("Prefabs")]
  public GameObject explosionAnimGO;

  // Start is called before the first frame update
  void Start()
  {

  }

  public void InitAttributes(float _speed, float _health)
  {
    speed = _speed;
    health = _health;
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
      health -= 5f;
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

    // Ajusta o tamanho da explosão para inimigos maiores
    if (gameObject.transform.localScale.x >= 1)
      explosionAnim.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

    explosionAnim.transform.position = transform.position;
  }
}
