using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object_", menuName = "ScriptableObjects/Object", order = 1)]
public class ObjectLifeData : ScriptableObject
{
  [Tooltip("The object's full life.")]
  public int fullLife;

  [Tooltip("Recovery time between damage.")]
  public float timeBetweenDamage;

  [Tooltip("The object's invulnerability time after damage.")]
  public bool invulnerableOnDamage = true;
}
