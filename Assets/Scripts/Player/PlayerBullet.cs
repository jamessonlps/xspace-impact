using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
  [SerializeField] private GameObject impactAnimGO;

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
