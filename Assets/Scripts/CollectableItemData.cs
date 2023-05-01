using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "ScriptableObject/Collectable Item")]
public class CollectableItemData : ScriptableObject
{
  public Sprite sprite;
  public CollectableItemType type;
}

public enum CollectableItemType
{
  Health,
  BulletPower,
  Laser
}