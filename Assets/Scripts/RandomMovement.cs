using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        var horizontal = Random.Range(-1.0f, 1.0f);
        var vertical = Random.Range(-1.0f, 1.0f);

        var _movement = new Vector2(horizontal, vertical).normalized;
        _rigidbody2D.linearVelocity = _movement * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
