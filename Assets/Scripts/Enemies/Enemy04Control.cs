using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04Control : MonoBehaviour
{
  [Header("Attributes")]
  public float speed;         // velocidade do inimigo
  public float speedShooting; // velocidade do inimigo quando está atirando

  [Header("Prefabs")]
  public GameObject bullet;
  public GameObject gunFireAnimGO;

  [Header("Bullet Spawn Points")]
  public GameObject bulletSpawnPoint01;
  public GameObject bulletSpawnPoint02;

  bool isMoving = true;
  bool isShooting = false;
  Animator anim;
  GameObject player;

  public void InitAttributes(float _speed, float _speedShooting)
  {
    speed = _speed;
    speedShooting = _speedShooting;
  }

  private void Start()
  {
    player = GameObject.Find("PlayerGO");
    anim = GetComponent<Animator>();
    Invoke("StartPreparingShootAnimation", 1f);
  }

  private void Update()
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
    // Instancia o tiro
    GameObject bullet01 = (GameObject)Instantiate(bullet);
    bullet01.transform.position = bulletSpawnPoint01.transform.position;

    GameObject bullet02 = (GameObject)Instantiate(bullet);
    bullet02.transform.position = bulletSpawnPoint02.transform.position;

    // Instancia a animação de tiro e faz ela seguir o sprite
    GameObject gunFireAnim01 = (GameObject)Instantiate(gunFireAnimGO);
    GameObject gunFireAnim02 = (GameObject)Instantiate(gunFireAnimGO);

    gunFireAnim01.AddComponent<FollowObject>().targetTransform = bulletSpawnPoint01.transform;
    gunFireAnim02.AddComponent<FollowObject>().targetTransform = bulletSpawnPoint02.transform;

    gunFireAnim01.transform.position = bulletSpawnPoint01.transform.position;
    gunFireAnim01.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    gunFireAnim01.transform.rotation = Quaternion.Euler(0, 0, 90f);

    gunFireAnim02.transform.position = bulletSpawnPoint02.transform.position;
    gunFireAnim02.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    gunFireAnim02.transform.rotation = Quaternion.Euler(0, 0, 90f);
  }

}
