using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCrystalControl : MonoBehaviour
{
  [SerializeField] private CollectableItem collectableItem;
  [SerializeField] private float speed = 5f;

  private Player player;

  private void Awake()
  {
    player = GameObject.FindObjectOfType<Player>();
    collectableItem.OnItemCollected += HandleGetCollectableItem;
  }

  private void OnDestroy() => collectableItem.OnItemCollected -= HandleGetCollectableItem;

  private void HandleGetCollectableItem()
  {
    if (player != null)
      player.IncreaseLife();
  }

  void Update()
  {
    Vector2 position = transform.position;
    position = new Vector2(position.x - speed * Time.deltaTime, position.y);
    transform.position = position;

    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    if (position.x < bottomLeft.x)
      Destroy(gameObject);
  }
}