using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04Control : MonoBehaviour
{
  bool isMoving = true;
  bool isShooting = false;

  public float speed = 7f;
  public float speedShooting = 1f;

  public GameObject bullet;
  public GameObject BulletSpawnPoint01;
  public GameObject BulletSpawnPoint02;

  GameObject player;

  Animator anim;

  void Start()
  {
    player = GameObject.Find("PlayerGO");
    anim = GetComponent<Animator>();
    Invoke("StartPreparingShootAnimation", 1f);
  }

  void Update()
  {
    if (isMoving)
    {
      Vector2 position = transform.position;
      position = new Vector2(position.x - speed * Time.deltaTime, position.y);
      transform.position = position;
    }

    if (isShooting)
    {
      Vector2 position = transform.position;
      Vector2 playerPosition = player.transform.position;

      // tamanho desse objeto
      float height = GetComponent<SpriteRenderer>().bounds.size.y;

      // se o player estiver na mesma altura do inimigo
      if ((position.y > playerPosition.y + (height / 2)) && (position.y < playerPosition.y - (height / 2)))
        position = new Vector2(position.x - speedShooting * Time.deltaTime, position.y);

      // se o player estiver acima do inimigo
      else if (playerPosition.y > position.y + (height / 2))
        position = new Vector2(position.x - speedShooting * Time.deltaTime, position.y + speed * Time.deltaTime);

      // se o player estiver abaixo do inimigo
      else if (playerPosition.y < position.y - (height / 2))
        position = new Vector2(position.x - speedShooting * Time.deltaTime, position.y - speed * Time.deltaTime);

      else
        position = new Vector2(position.x - speedShooting * Time.deltaTime, position.y);

      transform.position = position;
    }

    Vector2 rightTop = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    Vector2 leftBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

    // se o inimigo estiver no meio da tela, ele para de se mover
    if (transform.position.x < ((rightTop.x + leftBottom.x) / 2))
      speedShooting = 0f;
  }

  void StartPreparingShootAnimation()
  {
    anim.SetBool("isPreparingShoot", true);
    isMoving = false;
  }

  void UpdatePreparingToShootAnimation()
  {
    anim.SetBool("isPreparingShoot", false);
    anim.SetBool("isShooting", true);
    isShooting = true;
  }

  void OnStartShootingAnimation()
  {
    GameObject bullet01 = Instantiate(bullet);
    bullet01.transform.position = BulletSpawnPoint01.transform.position;

    GameObject bullet02 = Instantiate(bullet);
    bullet02.transform.position = BulletSpawnPoint02.transform.position;
  }
}
