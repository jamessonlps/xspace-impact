using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCrystalControl : MonoBehaviour
{
    [Header("Attributes")]
    public float speed;

    public void InitAttributes(float _speed)
    {
        speed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        // Atualiza a posição do crystal
        position = new Vector2(position.x - speed * Time.deltaTime, position.y);
        transform.position = position;

        // Remove o crystal quando ele sai da tela
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (position.x < bottomLeft.x)
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Detecta colisão com o a nave
        if (collider.tag == "PlayerTag")
        {
            Destroy(gameObject);
        }
        }
}