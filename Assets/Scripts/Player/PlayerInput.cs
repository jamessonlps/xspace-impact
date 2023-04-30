using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
  float doubleClickTimeThreshold = 0.3f;
  float lastKeyUpPressTime = 0f;
  float lastKeyDownPressTime = 0f;

  public Vector2 GetInputMovement()
  {
    Vector2 inputMovement = new Vector2(
        Input.GetAxisRaw("Horizontal"),
        Input.GetAxisRaw("Vertical")
    );
    return inputMovement;
  }

  public bool GetInputFire()
  {
    return Input.GetKeyDown("space");
  }

  public bool GetInputDodgeUp()
  {
    if (Input.GetKeyDown(KeyCode.UpArrow))
    {
      float timeSinceLastKeyUpPressed = Time.time - lastKeyUpPressTime;
      if (timeSinceLastKeyUpPressed <= doubleClickTimeThreshold)
      {
        lastKeyUpPressTime = Time.time;
        return true;
      }
      else
      {
        lastKeyUpPressTime = Time.time;
        return false;
      }
    }
    return false;
  }

  public bool GetInputDodgeDown()
  {
    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
      float timeSinceLastKeyDownPressed = Time.time - lastKeyDownPressTime;
      if (timeSinceLastKeyDownPressed <= doubleClickTimeThreshold)
      {
        lastKeyDownPressTime = Time.time;
        return true;
      }
      else
      {
        lastKeyDownPressTime = Time.time;
        return false;
      }
    }
    return false;
  }
}
