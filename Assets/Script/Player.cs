using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _movement;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        Debug.Log("Start Player");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _movement = new Vector2(horizontal, vertical).normalized;

        _animator.SetFloat("Horizontal", horizontal);
        _animator.SetFloat("Vertical", vertical);
        _animator.SetFloat("Velocity", _movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _movement * speed;
    }
}
