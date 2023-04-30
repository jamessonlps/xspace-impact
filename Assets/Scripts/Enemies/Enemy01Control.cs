using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Control : MonoBehaviour
{
  [Header("Attributes")]
  public float speed;

  [Header("Prefabs")]
  public GameObject explosionAnimGO;

  void Start()
  {

  }

  public void InitAttributes(float _speed)
  {
    speed = _speed;
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
