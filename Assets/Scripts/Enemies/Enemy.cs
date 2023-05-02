using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
  [Header("Audio Variables")]
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip bulletHitAudioClip;

  [Header("Animation Variables")]
  [SerializeField] private GameObject explosionAnim;
  [SerializeField] private Transform explosionAnimTransform;
  [SerializeField] private MaterialTintColor materialTintColor;
  [SerializeField] private Color damageTintColor = new Color(1, 0, 0, 1);

  private GameObject explosionAudioPlayer;

  public LifeManager lifeManager;

  private void Awake()
  {
    explosionAudioPlayer = GameObject.FindWithTag("ExplosionAudio");
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
    audioSource.clip = bulletHitAudioClip;
    audioSource.Play();
    materialTintColor.TintColor = damageTintColor;
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.TryGetComponent<Player>(out var player))
      player.TakeDamage(1);
  }

  private void PlayExplosionEffects()
  {
    explosionAudioPlayer.GetComponent<ExplosionAudio>().PlayExplosionAudio();
    GameObject explosion = (GameObject)Instantiate(explosionAnim);
    explosion.transform.position = explosionAnimTransform.position;
  }
}
