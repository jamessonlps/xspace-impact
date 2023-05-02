using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03Control : MonoBehaviour
{
  [SerializeField] private AudioSource audioSourceShoot;

  [Header("Attributes")]
  public float speed;
  public float shootingRate; // quantos segundos entre cada tiro
  public int maxNumOfShots;  // quantos tiros o inimigo deve dar antes de girar novamente

  [Header("Prefabs")]
  public GameObject bullet;    // prefab do tiro
  public GameObject explosion; // prefab da explosão

  [Header("Bullet Spawn Points")]
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


  Animator anim;
  int numOfShots; // quantos tiros o inimigo já deu

  public void InitAttributes(float _speed, int _maxNumOfShots, float _shootingRate)
  {
    this.speed = _speed;
    this.maxNumOfShots = _maxNumOfShots;
    this.shootingRate = _shootingRate;
  }

  private void Start()
  {
    numOfShots = 0;
    anim = GetComponent<Animator>();
    InitRotationAngles();
  }

  private void InitRotationAngles()
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

  private void Update()
  {
    if (anim.GetBool("isRotating"))
    {
      // atualiza a posição do inimigo
      Vector2 position = transform.position;
      position = new Vector2(position.x - speed * Time.deltaTime, position.y);
      transform.position = position;
    }

    // se está fora da tela, destrói o objeto
    Vector2 leftBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    if (transform.position.x < leftBottom.x)
    {
      Destroy(gameObject);
    }
  }

  private void ShootThemUp()
  {
    audioSourceShoot.Play();

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
      Invoke("ShootThemUp", shootingRate);
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
