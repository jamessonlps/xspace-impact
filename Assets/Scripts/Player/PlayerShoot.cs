using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerShoot : MonoBehaviour
{
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip shootSound;

  private int shootLevel = 1;

  [Header("Control Variables")]
  public float bulletSpeed;

  [Header("Prefabs")]
  public GameObject playerBullet;
  public GameObject gunFireAnimGO;

  [Header("Gun Fire Control")]
  public GameObject playerBulletPosition01;
  public GameObject playerBulletPosition02;
  public GameObject playerBulletPosition03;
  public GameObject playerBulletPosition04;
  public GameObject playerGunFirePosition01;
  public GameObject playerGunFirePosition02;

  PlayerInput playerInput;
  Animator playerAnimator;

  protected private void Awake()
  {
    playerInput = GetComponent<PlayerInput>();
    playerAnimator = GetComponent<Animator>();
  }

  private void Start()
  {
    bulletSpeed = 20f;
  }

  private void Update()
  {
    if (playerInput.GetInputFire())
      ShotOnThem();
  }

  /// <summary>
  /// Instancia as balas e as posiciona.
  /// </summary>
  private void ShotOnThem()
  {
    PlayAnimGunFire();
    PlayShootSong();
    playerAnimator.SetBool("isShooting", true);

    GameObject bullet01 = (GameObject)Instantiate(playerBullet);
    GameObject bullet02 = (GameObject)Instantiate(playerBullet);

    bullet01.GetComponent<PlayerBulletControl>().speed = bulletSpeed;
    bullet02.GetComponent<PlayerBulletControl>().speed = bulletSpeed;

    bullet01.transform.position = playerBulletPosition01.transform.position;
    bullet02.transform.position = playerBulletPosition02.transform.position;

    if (shootLevel >= 2)
    {
      GameObject bullet03 = (GameObject)Instantiate(playerBullet);
      GameObject bullet04 = (GameObject)Instantiate(playerBullet);

      bullet03.GetComponent<PlayerBulletControl>().speed = bulletSpeed;
      bullet04.GetComponent<PlayerBulletControl>().speed = bulletSpeed;

      bullet03.transform.position = playerBulletPosition03.transform.position;
      bullet04.transform.position = playerBulletPosition04.transform.position;
    }

    Invoke("StopShootingAnimation", 0.1f);
  }

  /// <summary>
  /// Instancia as animações de tiro e as posiciona, adicionando
  /// o script de follow object para que elas sigam o player.
  /// </summary>
  private void PlayAnimGunFire()
  {
    GameObject gunFireAnim01 = (GameObject)Instantiate(gunFireAnimGO);
    GameObject gunFireAnim02 = (GameObject)Instantiate(gunFireAnimGO);

    gunFireAnim01.AddComponent<FollowObject>().targetTransform = playerGunFirePosition01.transform;
    gunFireAnim02.AddComponent<FollowObject>().targetTransform = playerGunFirePosition02.transform;

    gunFireAnim01.transform.position = playerGunFirePosition01.transform.position;
    gunFireAnim02.transform.position = playerGunFirePosition02.transform.position;
  }

  private void PlayShootSong()
  {
    audioSource.clip = shootSound;
    audioSource.Play();
  }

  /// <summary>
  /// Método disparado quando a animação de tiro termina. Serve para
  /// mudar o estado da animação da animação de tiro para idle.
  /// </summary>
  void StopShootingAnimation()
  {
    playerAnimator.SetBool("isShooting", false);
  }


  public void UpgradeShootLevel()
  {
    if (shootLevel <= 3)
      shootLevel++;
  }

  public void DowngradeShootLevel()
  {
    if (shootLevel > 1)
      shootLevel--;
  }
}
