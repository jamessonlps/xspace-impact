using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
  public float speed;
  public float dodgeSpeed;

  float lastKeyUpPressTime = 0f;
  float lastKeyDownPressTime = 0f;
  float doubleClickTimeThreshold = 0.3f;

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
    dodgeSpeed = 15f;
    playerAnimator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown("space"))
      ShotOnThem();

    // Se não estiver se esquivando, pode se esquivar ou se mover
    if (!playerAnimator.GetBool("isDodgingUp") && !playerAnimator.GetBool("isDodgingDown"))
    {
      if (Input.GetKeyDown(KeyCode.W))
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

      if (Input.GetKeyDown(KeyCode.S))
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

    // calcula a nova posição
    position += _direction * speed * Time.deltaTime;

    // atualiza a posição do player
    transform.position = position;
  }

  // Método que controle a esquiva do player
  void ActiveDodgeUp()
  {
    playerAnimator.SetBool("isDodgingUp", true);
  }

  void ActiveDodgeDown()
  {
    playerAnimator.SetBool("isDodgingDown", true);
  }

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

  void StopShootingAnimation()
  {
    playerAnimator.SetBool("isShooting", false);
  }

  void StopDodgingUpAnimation()
  {
    playerAnimator.SetBool("isDodgingUp", false);
  }

  void StopDodgingDownAnimation()
  {
    playerAnimator.SetBool("isDodgingDown", false);
  }
}
