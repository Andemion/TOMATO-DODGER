using System.Collections;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    
    private Vector2 _movement;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    public float speed = 5;
    public int life = 3;
    [SerializeField] private LifeUI lifeUI;
    [SerializeField] private Collider2D floorCollider;
    // Champs pour le freeze
    private bool _isFrozen = false;
    private Coroutine _freezeRoutine;
    // Champs pour la glissade
    private bool    _isSliding;
    private Vector2 _slideDir;
    private float   _slideSpeed;
    private float   _slideTimeRemaining;
    private Coroutine _slideRoutine;
    public Vector2 LastMovementDirection => _movement;
    public Vector2 CurrentVelocity => _rigidbody2D.linearVelocity;
    public event Action OnDeath;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        if (lifeUI == null)
        {
            lifeUI = FindFirstObjectByType<LifeUI>();
        }
    }

    void Start()
    {
        if (lifeUI != null)
        {
            lifeUI.SetLives(life);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFrozen || _isSliding) return;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _movement = new Vector2(horizontal, vertical).normalized;

        _animator.SetFloat("Horizontal", horizontal);
        _animator.SetFloat("Vertical", vertical);
        _animator.SetFloat("Velocity", _movement.sqrMagnitude);

    }
    
    private void FixedUpdate()
    {
        if (_isFrozen) 
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            return;
        }
        if (_isSliding)
        {
            // On conserve la vélocité de glissade
            _rigidbody2D.linearVelocity = _slideDir * _slideSpeed;
            return;
        }
        _rigidbody2D.linearVelocity = _movement * speed;
    }
    
    public void ChangeLife(int delta)
    {
        life = Mathf.Max(0, life + delta);
        // Mets à jour l'UI via LifeUI, son, animation…
        UpdateLifeUI();
        if (life == 0)
            OnDeath?.Invoke();
    }

    private void UpdateLifeUI()
    {
        if (lifeUI != null)
        {
            lifeUI.SetLives(life);
        }
    }

    public void ResetLife()
    {
        life = 3;
        UpdateLifeUI();
    }
    
    public void TeleportOnFloorRandom()
    {
        if (floorCollider == null) return;

        // 1) Récupère les bounds du sol
        Bounds b = floorCollider.bounds;

        // 2) Choisis un X aléatoire entre les bords
        float x = Random.Range(b.min.x, b.max.x);

        // 3) Place Y juste au‑dessus du sol (on ajoute un petit offset si besoin)
        float y = b.max.y + GetComponent<Collider2D>().bounds.extents.y;

        // 4) Applique la nouvelle position
        transform.position = new Vector3(x, y, transform.position.z);
    }
    
    public void FreezeMovement(float duration)
    {
        if (_freezeRoutine != null)
            StopCoroutine(_freezeRoutine);
        _freezeRoutine = StartCoroutine(FreezeCoroutine(duration));
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        _isFrozen = true;
        yield return new WaitForSeconds(duration);
        _isFrozen = false;
        _freezeRoutine = null;
    }
    
    public void Slide(Vector2 direction, float speed, float duration)
    {
        // Stoppe une ancienne glissade si elle existe
        if (_slideRoutine != null) StopCoroutine(_slideRoutine);
        _slideRoutine = StartCoroutine(SlideCoroutine(direction.normalized, speed, duration));
    }

    private IEnumerator SlideCoroutine(Vector2 dir, float spd, float dur)
    {
        _isSliding = true;
        _slideDir = dir;
        _slideSpeed = spd;
        _slideTimeRemaining = dur;

        // Une frame pour initialiser la vélocité
        yield return null;

        while (_slideTimeRemaining > 0f)
        {
            _slideTimeRemaining -= Time.deltaTime;
            _rigidbody2D.linearVelocity = _slideDir * _slideSpeed;
            yield return null;
        }

        _isSliding = false;
        _slideRoutine = null;
    }
}
