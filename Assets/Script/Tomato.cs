using System;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public event Action<Tomato> OnWalk;
    private TomatoData _data;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public TomatoData Data
    {
        get => _data;
        set
        {
            _data = value;
            _spriteRenderer.color = _data.color;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){

        if (other.gameObject.CompareTag("Foot"))
        {
            OnWalk?.Invoke(this);
            Destroy(gameObject);
        }  
    }
}
