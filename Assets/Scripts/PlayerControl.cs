using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
  [Header("Player Movement Control")]
  public float speed;
  public float dodgeSpeed;
  public float bulletSpeed;

  float lastKeyUpPressTime = 0f;
  float lastKeyDownPressTime = 0f;
  float doubleClickTimeThreshold = 0.3f;

  [Header("Player Bullets")]
  public GameObject playerBullet;
  public GameObject playerBulletPosition01;
  public GameObject playerBulletPosition02;

  [Header("Player Gun Fire Animation")]
  public GameObject gunFireAnimGO;
  public GameObject playerGunFirePosition01;
  public GameObject playerGunFirePosition02;

  Animator playerAnimator;

  // Start is called before the first frame update
  void Start()
  {
    // TODO: atribuições devem ser feitas por um controlador
    speed = 10f;
    dodgeSpeed = 15f;
    bulletSpeed = 20f;
    playerAnimator = GetComponent<Animator>();
  }

  // TODO: separar as responsabilidades em scripts (PlayerControl, PlayerMovement, PlayerShooting, PlayerDodge, etc)

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown("space"))
      ShotOnThem();

    // Se não estiver se esquivando, pode se esquivar ou se mover
    if (!playerAnimator.GetBool("isDodgingUp") && !playerAnimator.GetBool("isDodgingDown"))
    {
      if (Input.GetKeyDown(KeyCode.UpArrow))
      {
        float timeSinceLastKeyUpPressed = Time.time - lastKeyUpPressTime;
        if (timeSinceLastKeyUpPressed <= doubleClickTimeThreshold)
        {
          ActiveDodgeUp();
          lastKeyUpPressTime = Time.time;
          return;
        }
        lastKeyUpPressTime = Time.time;
      }

      if (Input.GetKeyDown(KeyCode.DownArrow))
      {
        float timeSinceLastKeyDownPressed = Time.time - lastKeyDownPressTime;
        if (timeSinceLastKeyDownPressed <= doubleClickTimeThreshold)
        {
          ActiveDodgeDown();
          lastKeyDownPressTime = Time.time;
          return;
        }
        lastKeyDownPressTime = Time.time;
      }
    }

    // se estiver se esquivando, não pode se mover
    if (playerAnimator.GetBool("isDodgingUp"))
    {
      UpdateDodgeUpMovement();
      return;
    }

    // se estiver se esquivando, não pode se mover
    if (playerAnimator.GetBool("isDodgingDown"))
    {
      UpdateDodgeDownMovement();
      return;
    }

    // Movimenta o player normalmente
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

    // calcula a nova posição e atualiza
    position += _direction * speed * Time.deltaTime;
    transform.position = position;
  }

  // Ativa esquiva para cima
  void ActiveDodgeUp()
  {
    playerAnimator.SetBool("isDodgingUp", true);
  }

  // Ativa esquiva para baixo
  void ActiveDodgeDown()
  {
    playerAnimator.SetBool("isDodgingDown", true);
  }

  // Atualiza posição durante a esquiva para cima
  void UpdateDodgeUpMovement()
  {
    Vector2 position = transform.position;
    position.y += dodgeSpeed * Time.deltaTime;

    // garante que o player não saia da tela
    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    Vector2 playerSize = GetComponent<SpriteRenderer>().bounds.size;
    position = new Vector2(
      Mathf.Clamp(position.x, bottomLeft.x + playerSize.x / 2, topRight.x - playerSize.x / 2),
      Mathf.Clamp(position.y, bottomLeft.y + playerSize.y / 2, topRight.y - playerSize.y / 2)
    );

    transform.position = position;
  }


  // Atualiza posição durante a esquiva para baixo
  void UpdateDodgeDownMovement()
  {
    Vector2 position = transform.position;
    position.y -= dodgeSpeed * Time.deltaTime;

    // garante que o player não saia da tela
    Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    Vector2 playerSize = GetComponent<SpriteRenderer>().bounds.size;
    position = new Vector2(
      Mathf.Clamp(position.x, bottomLeft.x + playerSize.x / 2, topRight.x - playerSize.x / 2),
      Mathf.Clamp(position.y, bottomLeft.y + playerSize.y / 2, topRight.y - playerSize.y / 2)
    );

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

    bullet01.GetComponent<PlayerBulletControl>().speed = bulletSpeed;
    bullet02.GetComponent<PlayerBulletControl>().speed = bulletSpeed;

    // Posiciona os bullets
    bullet01.transform.position = playerBulletPosition01.transform.position;
    bullet02.transform.position = playerBulletPosition02.transform.position;

    Invoke("StopShootingAnimation", 0.15f);
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

  // Acionada quando encerra animação de disparo
  void StopShootingAnimation()
  {
    playerAnimator.SetBool("isShooting", false);
  }


  // Acionada quando encerra animação de esquiva para cima
  void StopDodgingUpAnimation()
  {
    playerAnimator.SetBool("isDodgingUp", false);
  }


  // Acionada quando encerra animação de esquiva para baixo
  void StopDodgingDownAnimation()
  {
    playerAnimator.SetBool("isDodgingDown", false);
  }
}
