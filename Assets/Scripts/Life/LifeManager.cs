using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeManager : MonoBehaviour
{
  public ObjectLifeData lifeData;

  public event Action<int> OnLifeChange; // informa às outras classes quando o item for alterado
  public event Action OnTakeDamage;      // informa às outras classes quando o item receber dano
  public event Action OnEndTakingDamage; // informa às outras classes quando o item parar de receber dano
  public event Action OnDeath;           // informa às outras classes quando o item for destruído

  private DateTime lastDamageTime; // armazena o tempo do último dano recebido
  private int life;
  private WaitForSeconds endTakingDamageWait; // armazena o tempo de espera para parar de receber dano

  public int Life
  {
    get { return life; }
    set
    {
      if (life < 0) return;
      life = value;
      OnLifeChange?.Invoke(life);       // dispara o evento OnLifeChange passando o valor de life
      if (life <= 0) OnDeath?.Invoke(); // dispara o evento OnDeath quando life for menor ou igual a zero
    }
  }

  private IEnumerator EndTakeDamage()
  {
    yield return endTakingDamageWait;
    OnEndTakingDamage?.Invoke();
  }

  private void Start()
  {
    Life = lifeData.fullLife;
    endTakingDamageWait = new WaitForSeconds(lifeData.timeBetweenDamage);
  }

  public bool TakeDamage(int damage)
  {
    if (!CanTakeDamage()) return false;
    this.Life -= damage;
    OnTakeDamage?.Invoke();
    StartCoroutine(EndTakeDamage());
    lastDamageTime = DateTime.UtcNow;
    return true;
  }

  public bool CanTakeDamage()
  {
    if (!lifeData.invulnerableOnDamage) return true;
    if (lifeData.timeBetweenDamage > 0)
    {
      TimeSpan timeSpan = DateTime.UtcNow - lastDamageTime;
      return timeSpan.TotalSeconds > lifeData.timeBetweenDamage;
    }
    return true;
  }
}
