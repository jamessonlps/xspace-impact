using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] private GameObject explosionAnim;
  [SerializeField] private Transform explosionAnimTransform;

  public LifeManager lifeManager;

  private void Awake()
  {
    lifeManager.OnDeath += HandleDeath;
  }

  private void OnDestroy()
  {
    lifeManager.OnDeath -= HandleDeath;
  }

  private void HandleDeath()
  {
    PlayExplosionEffects();
    Destroy(gameObject);
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.TryGetComponent<PlayerControl>(out var player))
    {
      // TODO: Tirar vida do player
      Debug.Log("Tirar vida do Player");
    }
  }

  private void PlayExplosionEffects()
  {
    GameObject explosion = (GameObject)Instantiate(explosionAnim);
    explosion.transform.position = explosionAnimTransform.position;
  }
}
