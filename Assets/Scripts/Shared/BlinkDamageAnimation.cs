using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class BlinkDamageAnimation : MonoBehaviour
{
  [SerializeField] private float blinkTime = 0.5f;

  private SpriteRenderer spriteRenderer;
  private Tweener tweener;

  private void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public void StartAnimation()
  {
    tweener = spriteRenderer.DOFade(0, blinkTime).SetLoops(-1);
  }

  public void EndAnimation()
  {
    tweener?.Kill();
    spriteRenderer.color = Color.white;
  }
}
