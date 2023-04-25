using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
  public float speed;

  public GameObject playerBullet;
  public GameObject playerBulletPosition01;
  public GameObject playerBulletPosition02;

  public GameObject gunFireAnimGO;
  public GameObject playerGunFirePosition01;
  public GameObject playerGunFirePosition02;

  public Animator playerAnimator;



  // Start is called before the first frame update
  void Start()
  {
    speed = 10f;
    playerAnimator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown("space"))
      ShotOnThem();

    // pega o input do usuário
    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");

    // calcula a direção
    Vector2 direction = new Vector2(x, y).normalized;

    MovePlayer(direction);
  }

  // Método que controla o movimento do player
  void MovePlayer(Vector2 _direction)
  {
    // posição atual do player
    Vector2 position = transform.position;

    // dimensões da tela
    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    // tamanho do player
    Vector2 playerSize = GetComponent<SpriteRenderer>().bounds.size;

    // garante que o player não saia da tela
    position = new Vector2(
      Mathf.Clamp(position.x, bottomLeft.x + playerSize.x / 2, topRight.x - playerSize.x / 2),
      Mathf.Clamp(position.y, bottomLeft.y + playerSize.y / 2, topRight.y - playerSize.y / 2)
    );

    // calcula a nova posição
    position += _direction * speed * Time.deltaTime;

    // atualiza a posição do player
    transform.position = position;
  }

  // Método que controla o tiro do player
  void ShotOnThem()
  {
    // Inicia a animação de tiro
    PlayAnimGunFire();
    playerAnimator.SetBool("isShooting", true);

    // Instancia os bullets
    GameObject bullet01 = (GameObject)Instantiate(playerBullet);
    GameObject bullet02 = (GameObject)Instantiate(playerBullet);

    // Posiciona os bullets
    bullet01.transform.position = playerBulletPosition01.transform.position;
    bullet02.transform.position = playerBulletPosition02.transform.position;

    Invoke("StopShootingAnim", 0.15f);
  }

  void PlayAnimGunFire()
  {
    // Instancia as animações de tiro
    GameObject gunFireAnim01 = (GameObject)Instantiate(gunFireAnimGO);
    GameObject gunFireAnim02 = (GameObject)Instantiate(gunFireAnimGO);

    // Adiciona o script de follow object nas animações
    // para que elas fiquem posicionadas corretamente
    gunFireAnim01.AddComponent<FollowObject>().targetTransform = playerGunFirePosition01.transform;
    gunFireAnim02.AddComponent<FollowObject>().targetTransform = playerGunFirePosition02.transform;

    // Posiciona as animações
    gunFireAnim01.transform.position = playerGunFirePosition01.transform.position;
    gunFireAnim02.transform.position = playerGunFirePosition02.transform.position;
  }

  void StopShootingAnim()
  {
    playerAnimator.SetBool("isShooting", false);
  }
}
