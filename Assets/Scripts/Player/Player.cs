using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
  [Header("Audio")]
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private GameObject audioExplosionGO;

  [Header("Life")]
  [SerializeField] private LifeManager lifeManager;
  [SerializeField] private TMP_Text lifeText;

  [Header("Gameplay Manager")]
  [SerializeField] private GameplayManager gameplayManager;

  [Header("Explosion Animation")]
  [SerializeField] private GameObject exposionAnimation;

  private BlinkDamageAnimation blinkDamageAnimation;

  private void Awake()
  {
    blinkDamageAnimation = GetComponent<BlinkDamageAnimation>();

    lifeManager.OnDeath += HandleDeath;
    lifeManager.OnLifeChange += HandleLifeChange;
    lifeManager.OnTakeDamage += HandleTakeDamage;
    lifeManager.OnEndTakingDamage += HandleEndTakingDamage;
  }

  private void OnDestroy()
  {
    lifeManager.OnDeath -= HandleDeath;
    lifeManager.OnLifeChange -= HandleLifeChange;
    lifeManager.OnTakeDamage -= HandleTakeDamage;
    lifeManager.OnEndTakingDamage -= HandleEndTakingDamage;
  }

  private void HandleTakeDamage()
  {
    gameObject.GetComponent<PlayerShoot>().DowngradeShootLevel();
    audioSource.Play();
    blinkDamageAnimation.StartAnimation();
  }

  private void HandleEndTakingDamage()
  {
    blinkDamageAnimation.EndAnimation();
  }

  private void HandleDeath()
  {
    GameObject explosion = (GameObject)Instantiate(exposionAnimation, transform.position, Quaternion.identity);
    explosion.transform.localScale = new Vector3(2f, 2f, 2f);
    audioExplosionGO.GetComponent<AudioSource>().Play();
    gameplayManager.ChangeToGameOver();
    Destroy(gameObject);
  }

  private void HandleLifeChange(int life)
  {
    if (life >= 0)
      lifeText.text = life.ToString();
  }


  public bool TakeDamage(int damage)
  {
    return lifeManager.TakeDamage(damage);
  }

  public bool IncreaseLife()
  {
    return lifeManager.IncreaseLife();
  }
}
