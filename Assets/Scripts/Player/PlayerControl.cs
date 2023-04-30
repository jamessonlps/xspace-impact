using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerControl : MonoBehaviour
{
  [Header("Player Movement Control")]
  public float speed;

  [SerializeField] private float dodgeSpeed;

  Animator playerAnimator;
  PlayerInput playerInput;

  protected private void Awake()
  {
    playerAnimator = GetComponent<Animator>();
    playerInput = GetComponent<PlayerInput>();
  }

  void Start()
  {
    // TODO: atribuições devem ser feitas por um controlador
    speed = 10f;
    dodgeSpeed = 15f;
  }

  void Update()
  {
    // Se não estiver se esquivando, pode se esquivar ou se mover
    if (!playerAnimator.GetBool("isDodgingUp") && !playerAnimator.GetBool("isDodgingDown"))
    {
      if (playerInput.GetInputDodgeUp())
        ActiveDodgeUp();
      else if (playerInput.GetInputDodgeDown())
        ActiveDodgeDown();
      else
        MovePlayer(playerInput.GetInputMovement());
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
