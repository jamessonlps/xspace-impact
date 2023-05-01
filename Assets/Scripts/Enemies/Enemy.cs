using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
  [SerializeField] private GameObject explosionAnim;
  [SerializeField] private Transform explosionAnimTransform;
  [SerializeField] private MaterialTintColor materialTintColor;
  [SerializeField] private Color damageTintColor = new Color(1, 0, 0, 1);

  public LifeManager lifeManager;

  private void Awake()
  {
    lifeManager.OnTakeDamage += HandleTakeDamage;
    lifeManager.OnDeath += HandleDeath;
  }

  private void OnDestroy()
  {
    lifeManager.OnTakeDamage -= HandleTakeDamage;
    lifeManager.OnDeath -= HandleDeath;
  }

  private void HandleDeath()
  {
    PlayExplosionEffects();
    Destroy(gameObject);
  }

  private void HandleTakeDamage()
  {
    materialTintColor.TintColor = damageTintColor;
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.TryGetComponent<Player>(out var player))
      player.TakeDamage(1);
  }

  private void PlayExplosionEffects()
  {
    GameObject explosion = (GameObject)Instantiate(explosionAnim);
    explosion.transform.position = explosionAnimTransform.position;
  }
}
