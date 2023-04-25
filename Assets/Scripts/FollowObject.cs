using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
  public Transform targetTransform;

  void Update()
  {
    transform.position = targetTransform.position;
  }
}
