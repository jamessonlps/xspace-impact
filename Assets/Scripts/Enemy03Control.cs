using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03Control : MonoBehaviour
{
  Animator anim;

  int rotationCount; // quantas vezes o inimigo já girou
  int maxRotationCount = 2; // quantas vezes o inimigo deve girar antes de atirar

  public GameObject bullet; // prefab do tiro
  public GameObject bulletSpawnPoint01; // objeto que indica onde o tiro deve aparecer
  public GameObject bulletSpawnPoint02;
  public GameObject bulletSpawnPoint03;
  public GameObject bulletSpawnPoint04;

  // Start is called before the first frame update
  void Start()
  {
    rotationCount = 0;
    anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void UpdateRotationAnimState()
  {
    rotationCount++;
    if (rotationCount >= maxRotationCount)
    {
      rotationCount = 0;
      anim.SetBool("isPreparingToShoot", true);
      anim.SetBool("isRotating", false);
    }
  }

  public void UpdateReadyToShootAnimState()
  {
    anim.SetBool("isPreparingToShoot", false);
    anim.SetBool("isShooting", true);

    ShootThemUp();
  }

  void ShootThemUp()
  {
    // instancia os tiros
    GameObject bullet01 = (GameObject)Instantiate(bullet);
    bullet01.transform.position = bulletSpawnPoint01.transform.position;

    GameObject bullet02 = (GameObject)Instantiate(bullet);
    bullet02.transform.position = bulletSpawnPoint02.transform.position;

    GameObject bullet03 = (GameObject)Instantiate(bullet);
    bullet03.transform.position = bulletSpawnPoint03.transform.position;

    GameObject bullet04 = (GameObject)Instantiate(bullet);
    bullet04.transform.position = bulletSpawnPoint04.transform.position;

    Invoke("ShootThemUp", 1f);
  }


}
