using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
  [SerializeField] private GameObject impactAnimGO;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.TryGetComponent<Player>(out var player))
    {
      PlayImpactAnimation();
      Destroy(gameObject);
      player.TakeDamage(1);
    }
    if (other.tag == "PlayerBulletTag")
    {
      PlayImpactAnimation();
      Destroy(gameObject);
    }
  }

  private void PlayImpactAnimation()
  {
    GameObject impactAnim = (GameObject)Instantiate(impactAnimGO);
    impactAnim.transform.position = transform.position;
  }
}
