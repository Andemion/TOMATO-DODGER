using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class TomatoManager : MonoBehaviour
{
    [Header("Spawn Setup")]
    [SerializeField] private Transform spawner;
    [SerializeField] private Collider2D floorCollider;
    [SerializeField] private Transform container;
    [SerializeField] private Tomato prefab;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private float spawnHeight = 2f;

    [Header("Data")]
    [SerializeField] private List<TomatoData> tomatoDataList;

    [Header("Visée")]
    [Tooltip("Transform du Player")]
    public Transform playerTransform;
    
    [Tooltip("Angle max d'erreur en degrés")]
    [Range(0f, 90f)]
    public float aimErrorAngle = 15f;
    
    private readonly List<Tomato> _tomatoes = new List<Tomato>();
    private Coroutine _spawnRoutine;

    public event Action<Tomato> OnCollected;
    

    public void StartSpawning()
    {
        if (_spawnRoutine == null)
            _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Spawn()
    {
        // 1) Choix aléatoire du data
        var data = tomatoDataList[Random.Range(0, tomatoDataList.Count)];

        // 2) Position de spawn : un X au hasard dans les bounds de ta Floor, et Y au‑dessus
        Bounds b = floorCollider.bounds;
        float x = Random.Range(b.min.x, b.max.x);
        float y = b.max.y + spawnHeight;
        Vector2 spawnPos = new Vector2(x, y);

        // 3) Instanciation
        var tomato = Instantiate(prefab, spawnPos, Quaternion.identity, container);
        tomato.Data = data;

        // 4) Calcul de la direction “vers le Player” + erreur
        Vector2 toPlayer   = ((Vector2)playerTransform.position - spawnPos).normalized;
        float   errorAngle = Random.Range(-aimErrorAngle, aimErrorAngle);
        Vector2 finalDir   = RotateVector(toPlayer, errorAngle);

        // 5) Applique la vitesse / force correctement
        var rb = tomato.GetComponent<Rigidbody2D>();

        // OPTION B) Arc par gravité + impulsion initiale
        rb.gravityScale = 0.2f;                // ou autre valeur
        rb.AddForce(finalDir * data.launchForce, ForceMode2D.Impulse);

        // 6) Enregistrement pour la collecte
        AddTomato(tomato);
    }


    private void AddTomato(Tomato tomato)
    {
        _tomatoes.Add(tomato);
        tomato.OnCollected += TomatoCollectedHandler;
        //tomato.OnMissed    += TomatoMissedHandler;
    }

    private void RemoveTomato(Tomato tomato)
    {
        tomato.OnCollected -= TomatoCollectedHandler;
        //tomato.OnMissed    -= TomatoMissedHandler;
        _tomatoes.Remove(tomato);
        Destroy(tomato.gameObject);
    }

    private void TomatoCollectedHandler(Tomato tomato)
    {
        OnCollected?.Invoke(tomato);
        RemoveTomato(tomato);
    }

    private void TomatoMissedHandler(Tomato tomato)
    {
        // Ici la tomate a frappé le sol
        RemoveTomato(tomato);
    }
    
    private Vector2 RotateVector(Vector2 v, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }
    
}
