using System;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
  public event Action OnItemCollected;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.TryGetComponent<Player>(out var player))
    {
      OnItemCollected?.Invoke();
      // TODO: Animação do coletável sendo destruído
      Destroy(gameObject);
    }
  }
}
