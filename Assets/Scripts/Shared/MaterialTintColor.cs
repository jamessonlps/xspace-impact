using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTintColor : MonoBehaviour
{
  private int colorPropertyId = Shader.PropertyToID("_Color");
  private SpriteRenderer spriteRenderer;
  private MaterialPropertyBlock block;
  private Color tintColor = new Color(1, 1, 1, 0);

  [SerializeField] private float tintFadeSpeed = 2.5f;

  public Color TintColor
  {
    get { return tintColor; }
    set { tintColor = value; }
  }

  void Awake()
  {
    block = new MaterialPropertyBlock();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    if (tintColor.a > 0)
    {
      spriteRenderer.GetPropertyBlock(block);
      tintColor.a = Mathf.Clamp01(tintColor.a - tintFadeSpeed * Time.deltaTime);
      block.SetColor(colorPropertyId, tintColor);
      spriteRenderer.SetPropertyBlock(block);
    }
  }
}
