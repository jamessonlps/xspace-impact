using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletControl : MonoBehaviour
{
  [Header("Attributes")]
  public float speed;

  [Header("Prefabs")]
  public GameObject impactAnimGO;

  void Update()
  {
    Vector2 position = transform.position;
    position = new Vector2(position.x + speed * Time.deltaTime, position.y);
    transform.position = position;

    // se o bullet sair da área visível, é destruído
    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    if ((transform.position.x < bottomLeft.x) || (transform.position.x > topRight.x) ||
        (transform.position.y < bottomLeft.y) || (transform.position.y > topRight.y))
      Destroy(gameObject);
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.TryGetComponent<Enemy>(out var enemy))
    {
      PlayImpactAnimation();
      Destroy(gameObject);
      enemy.lifeManager.TakeDamage(1);
    }
    if (collider.tag == "EnemyBulletTag")
    {
      PlayImpactAnimation();
      Destroy(gameObject);
    }
  }

  void PlayImpactAnimation()
  {
    GameObject impactAnim = (GameObject)Instantiate(impactAnimGO);
    impactAnim.transform.position = transform.position;
  }
}
