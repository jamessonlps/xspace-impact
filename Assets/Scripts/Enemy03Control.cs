using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03Control : MonoBehaviour
{
  float speed;

  Animator anim;

  int rotationCount; // quantas vezes o inimigo já girou
  int maxRotationCount = 1; // quantas vezes o inimigo deve girar antes de atirar

  int numOfShots; // quantos tiros o inimigo já deu
  int maxNumOfShots = 5; // quantos tiros o inimigo deve dar antes de girar novamente

  public GameObject bullet; // prefab do tiro
  public GameObject bulletSpawnPoint01; // objeto que indica onde o tiro deve aparecer
  public GameObject bulletSpawnPoint02;
  public GameObject bulletSpawnPoint03;
  public GameObject bulletSpawnPoint04;

  // ângulo de rotação
  float angleBullet01;
  float angleBullet02;
  float angleBullet03;
  float angleBullet04;

  // direção do tiro
  Vector2 dirBullet01;
  Vector2 dirBullet02;
  Vector2 dirBullet03;
  Vector2 dirBullet04;


  // Start is called before the first frame update
  void Start()
  {
    numOfShots = 0;
    speed = 4f;
    anim = GetComponent<Animator>();
    InitRotationAngles();
  }

  void InitRotationAngles()
  {
    dirBullet01 = new Vector2(-1f, 0.33f);
    angleBullet01 = 180 + Mathf.Atan2(dirBullet01.y, dirBullet01.x) * Mathf.Rad2Deg;

    dirBullet02 = new Vector2(-1f, 0.2f);
    angleBullet02 = 180 + Mathf.Atan2(dirBullet02.y, dirBullet02.x) * Mathf.Rad2Deg;

    dirBullet03 = new Vector2(-1f, -0.2f);
    angleBullet03 = 180 + Mathf.Atan2(dirBullet03.y, dirBullet03.x) * Mathf.Rad2Deg;

    dirBullet04 = new Vector2(-1f, -0.33f);
    angleBullet04 = 180 + Mathf.Atan2(dirBullet04.y, dirBullet04.x) * Mathf.Rad2Deg;
  }

  void Update()
  {
    if (anim.GetBool("isRotating"))
    {
      // atualiza a posição do inimigo
      Vector2 position = transform.position;
      position = new Vector2(position.x - speed * Time.deltaTime, position.y);
      transform.position = position;
    }
  }

  void ShootThemUp()
  {
    // instancia os tiros
    GameObject bullet01 = (GameObject)Instantiate(bullet);
    bullet01.transform.position = bulletSpawnPoint01.transform.position;
    bullet01.transform.rotation = Quaternion.Euler(0, 0, angleBullet01);
    bullet01.GetComponent<Enemy03Bullet>().SetDirection(dirBullet01);

    GameObject bullet02 = (GameObject)Instantiate(bullet);
    bullet02.transform.position = bulletSpawnPoint02.transform.position;
    bullet02.transform.rotation = Quaternion.Euler(0, 0, angleBullet02);
    bullet02.GetComponent<Enemy03Bullet>().SetDirection(dirBullet02);

    GameObject bullet03 = (GameObject)Instantiate(bullet);
    bullet03.transform.position = bulletSpawnPoint03.transform.position;
    bullet03.transform.rotation = Quaternion.Euler(0, 0, angleBullet03);
    bullet03.GetComponent<Enemy03Bullet>().SetDirection(dirBullet03);

    GameObject bullet04 = (GameObject)Instantiate(bullet);
    bullet04.transform.position = bulletSpawnPoint04.transform.position;
    bullet04.transform.rotation = Quaternion.Euler(0, 0, angleBullet04);
    bullet04.GetComponent<Enemy03Bullet>().SetDirection(dirBullet04);

    numOfShots++;

    if (numOfShots >= maxNumOfShots)
    {
      numOfShots = 0;
      anim.SetBool("isShooting", false);
      anim.SetBool("isPreparingToRotate", true);
    }
    else
      Invoke("ShootThemUp", 0.5f);
  }


  public void UpdateRotationAnimState()
  {
    anim.SetBool("isRotating", false);
    anim.SetBool("isPreparingToShoot", true);
  }

  public void UpdateReadyToShootAnimState()
  {
    anim.SetBool("isPreparingToShoot", false);
    anim.SetBool("isShooting", true);

    ShootThemUp();
  }

  public void UpdateReadyToRotate()
  {
    anim.SetBool("isPreparingToRotate", false);
    anim.SetBool("isRotating", true);
  }

}
