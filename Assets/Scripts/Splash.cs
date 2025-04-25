using System;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public event Action<Splash> OnCollected;
    private SplashData _data; 
    private SpriteRenderer _spriteRenderer;
    Player player;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindFirstObjectByType<Player>();
        if (player == null)
            Debug.LogError("Splash: Aucun Player trouvé dans la scène !");
    }
    
    public SplashData Data
    {
        get => _data;
        set
        {
            _data = value;
            if (_spriteRenderer != null && _data != null)
                _spriteRenderer.sprite = _data.image;
            
            if (_data != null && _data.lifeDelta < 0)
            {
                // auto‑destruction du splash orange au bout de 5 s
                Destroy(gameObject, 5f);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_data.teleportsPlayer)
            {
                player.TeleportOnFloorRandom();
            }
            else if (_data.lifeDelta < 0)
            {
                other.gameObject.GetComponent<Player>().ChangeLife(Data.lifeDelta);
                Destroy(gameObject);
            }
            else if (_data.freezeDuration > 0)
            {
                player.FreezeMovement(_data.freezeDuration);
            }
            else if (_data.slideDuration > 0 && _data.slideSpeed > 0)
            {
                Vector2 dir;
                if (player.LastMovementDirection.sqrMagnitude > 0.1f)
                    dir = player.LastMovementDirection;
                else
                    dir = player.CurrentVelocity.normalized;

                player.Slide(dir, _data.slideSpeed, _data.slideDuration);
            }

            OnCollected?.Invoke(this);
        }
    }
}
