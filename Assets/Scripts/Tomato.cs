using System;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public event Action<Tomato> OnCollected;
    private TomatoData _data;
    private SpriteRenderer _spriteRenderer;
    Rigidbody2D   rb;
    public CircleCollider2D col;
    Vector3 startScale;
    public float flightDuration = 5f;      // approximatif
    float elapsed = 0f;
    Player          player;
    public float minScale = 0.2f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rb  = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
        startScale = transform.localScale;
        player = FindFirstObjectByType<Player>();
        if (player == null)
            Debug.LogError("Pas de Player dans la scène !");
    }

    void Update()
    {
        // Si on n’a pas encore activé le collider
        if (!col.enabled && transform.position.y <= player.transform.position.y)
        {
            
        }
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / flightDuration);
        float shrinkFactor = Mathf.Lerp(1f, minScale, t);  // passe de 100% à 20%
        transform.localScale = startScale * shrinkFactor;
        if (!col.enabled && shrinkFactor <= minScale + 0.001f)
        {
            col.enabled = true;
        }
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        if (!col.enabled) return;   
        if (hit.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            // effet “écrasé” → destroy + particules…
            Destroy(gameObject);
        }
        if (hit.gameObject.CompareTag("Player"))
        {
            hit.gameObject.GetComponent<Player>().LoseLife(1);
            Destroy(gameObject);
        }
    }
    
    public TomatoData Data
    {
        get => _data;
        set
        {
            _data = value;
            _spriteRenderer.sprite = _data.image;
        }
    }
}
