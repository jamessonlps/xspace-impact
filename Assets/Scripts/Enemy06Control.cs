using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy06Control : MonoBehaviour
{
  public float speed;

  public GameObject enemyBullet;
  public GameObject enemyBulletPosition01;
  public GameObject enemyBulletPosition02;
  public GameObject enemyBulletPosition03;
  public GameObject enemyBulletPosition04;

  bool isShootRotationCompleted;
  float currentAngleRotation = 0f;
  float maxAngleRotation = 360f;
  float rotationSpeed = 36f;

  Animator enemyAnimator;

  void Start()
  {
    speed = 2.5f;
    isShootRotationCompleted = false;
    enemyAnimator = GetComponent<Animator>();
  }

  void Update()
  {
    if (enemyAnimator.GetBool("isRotating"))
      MoveEnemy();
    else
      RotateWhileShooting();

    // Se o inimigo sair da tela, destrua-o
    Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    if (transform.position.y < min.y)
      Destroy(gameObject);
  }

  void ShootOnThem()
  {
    if (isShootRotationCompleted)
      return;

    // Dispara os tiros usando os eixos dos bulletPositions
    GameObject bullet01 = (GameObject)Instantiate(enemyBullet);
    bullet01.transform.position = enemyBulletPosition01.transform.position;
    bullet01.GetComponent<BulletCircleControl>().SetDirection(enemyBulletPosition01.transform.up);

    GameObject bullet02 = (GameObject)Instantiate(enemyBullet);
    bullet02.transform.position = enemyBulletPosition02.transform.position;
    bullet02.GetComponent<BulletCircleControl>().SetDirection(-enemyBulletPosition02.transform.up);

    GameObject bullet03 = (GameObject)Instantiate(enemyBullet);
    bullet03.transform.position = enemyBulletPosition03.transform.position;
    bullet03.GetComponent<BulletCircleControl>().SetDirection(enemyBulletPosition03.transform.right);

    GameObject bullet04 = (GameObject)Instantiate(enemyBullet);
    bullet04.transform.position = enemyBulletPosition04.transform.position;
    bullet04.GetComponent<BulletCircleControl>().SetDirection(-enemyBulletPosition04.transform.right);

    Invoke("ShootOnThem", 0.3f);
  }

  void RotateWhileShooting()
  {
    if (currentAngleRotation < maxAngleRotation)
    {
      currentAngleRotation += rotationSpeed * Time.deltaTime;
      transform.rotation = Quaternion.Euler(0, 0, currentAngleRotation);
    }
    else
    {
      isShootRotationCompleted = true;
      currentAngleRotation = 0f;
      enemyAnimator.SetBool("isRotating", true);
    }
  }

  void MoveEnemy()
  {
    Vector2 position = transform.position;
    position = new Vector2(position.x - speed * Time.deltaTime, position.y);

    transform.position = position;
  }

  void OnTriggerEnter2D(Collider2D col)
  {
    // TODO: implementar colisões
  }


  void UpdateRotationAnimState()
  {
    enemyAnimator.SetBool("isRotating", false);

    isShootRotationCompleted = false;
    ShootOnThem();
  }
}
