using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private LifeManager lifeManager;
  [SerializeField] private TMP_Text lifeText;

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
    lifeManager.OnTakeDamage += HandleTakeDamage;
    lifeManager.OnEndTakingDamage += HandleEndTakingDamage;
  }

  private void HandleTakeDamage()
  {
    blinkDamageAnimation.StartAnimation();
  }

  private void HandleEndTakingDamage()
  {
    blinkDamageAnimation.EndAnimation();
  }

  private void HandleDeath()
  {
    // TODO: implementar morte do player
    Debug.Log("Player morreu");
  }

  private void HandleLifeChange(int life)
  {
    if (life >= 0)
      lifeText.text = life.ToString();
  }


  public bool TakeDamage(int damage)
  {
    // TODO: implementar o take damage nos bullets dos inimigos
    return lifeManager.TakeDamage(damage);
  }
}
