using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _movement;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    // private BoxCollider2D _bodyCollider;
    // private BoxCollider2D _footCollider;
    public float speed = 5;
    public int life = 3;
    [SerializeField] private LifeUI lifeUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        // _bodyCollider = transform.Find("Body").GetComponent<BoxCollider2D>();
        // _footCollider = transform.Find("Foot").GetComponent<BoxCollider2D>();
        if (lifeUI == null)
        {
            lifeUI = FindFirstObjectByType<LifeUI>();
        }
    }

    void Start()
    {
        Debug.Log("Start Player");
        if (lifeUI != null)
        {
            lifeUI.SetLives(life);
        }
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoseLife(1);
        }
    }
    
    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _movement * speed;
    }
    
    
    /// <summary>
    /// Enlève un certain nombre de vies et met à jour l'UI.
    /// </summary>
    public void LoseLife(int amount)
    {
        life = Mathf.Max(life - amount, 0);
        UpdateLifeUI();
        // Tu peux ici ajouter des effets (son, animation, game over…)
    }
    
    /// <summary>
    /// Ajoute un certain nombre de vies et met à jour l'UI.
    /// </summary>
    public void GainLife(int amount)
    {
        life += amount;
        UpdateLifeUI();
    }
    
    /// <summary>
    /// Centralise la mise à jour de l'UI vie.
    /// </summary>
    private void UpdateLifeUI()
    {
        if (lifeUI != null)
        {
            lifeUI.SetLives(life);
        }
    }
}
