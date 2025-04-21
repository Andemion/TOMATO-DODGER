using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class TomatoManager : MonoBehaviour
{
    [Header("Spawn Setup")]
    [SerializeField] private Collider2D floorCollider;
    [SerializeField] private Transform container;
    [SerializeField] private Tomato tomatoPrefab;
    [SerializeField] private Splash splashPrefab;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private float launchForce = 10f;
    
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
    public event Action<Splash> OnCollected;
    

    public void StartSpawning()
    {
        DestroyAllSplashs();
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
        // 1) Choix aléatoire pondéré des datas
        var data = GetRandomDataByProbability();

        // 2) Position de spawn : un X au hasard dans les bounds de ta Floor, et Y au‑dessus
        Bounds b = floorCollider.bounds;
        float x = Random.Range(b.min.x, b.max.x);
        float y = b.max.y + Random.Range(22f, 24f);
        Vector2 spawnPos = new Vector2(x, y);

        // 3) Instanciation
        var tomato = Instantiate(tomatoPrefab, spawnPos, Quaternion.identity, container);
        tomato.Data = data;

        // 4) Calcul de la direction “vers le Player” + erreur
        Vector2 toPlayer   = ((Vector2)playerTransform.position - spawnPos).normalized;
        float   errorAngle = Random.Range(-aimErrorAngle, aimErrorAngle);
        Vector2 finalDir   = RotateVector(toPlayer, errorAngle);

        // 5) Applique la vitesse / force correctement
        var rb = tomato.GetComponent<Rigidbody2D>();

        // Arc par gravité + impulsion initiale
        rb.gravityScale = 0.2f;
        rb.AddForce(finalDir * launchForce, ForceMode2D.Impulse);

        // 6) Enregistrement pour la collecte
        AddTomato(tomato);
    }
    
    private void AddTomato(Tomato tomato)
    {
        _tomatoes.Add(tomato);
        tomato.OnSplashed += TomatoSplashedHandler;
    }

    private void TomatoSplashedHandler(Tomato tomato, Vector2 contactPoint)
    {
        // Instancie le prefab Splash
        Splash splashInstance = Instantiate(
            splashPrefab, 
            contactPoint, 
            Quaternion.identity, 
            container
        );
        splashInstance.Data = tomato.Data.splashData;

        // Abonne‑toi à son event
        splashInstance.OnCollected += SplashCollectedHandler;
        
        // 2) Nettoyer
        tomato.OnSplashed -= TomatoSplashedHandler;
        _tomatoes.Remove(tomato);
        Destroy(tomato.gameObject);
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
    
    private TomatoData GetRandomDataByProbability()
    {
        // 1) Somme des poids
        float total = 0f;
        foreach (var d in tomatoDataList)
            total += d.probability;

        // 2) Tirage d'un nombre aléatoire dans [0, total[
        float roll = Random.Range(0f, total);

        // 3) On parcourt la liste en soustrayant chaque poids
        foreach (var d in tomatoDataList)
        {
            if (roll < d.probability)
                return d;
            roll -= d.probability;
        }

        // (par sécurité)
        return tomatoDataList[0];
    }
    
    private void SplashCollectedHandler(Splash splash)
    {
        OnCollected?.Invoke(splash);
        Destroy(splash.gameObject);
    }
    
    public void DestroyAllSplashs()
    {
        // Trouver tous les objets qui correspondent à "splashPrefab" et les supprimer
        GameObject[] splashObjects = GameObject.FindGameObjectsWithTag("Splash");
        
        foreach (GameObject splash in splashObjects)
        {
            Destroy(splash);
        }
    }
}
