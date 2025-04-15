using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
 
public class TomatoManager : MonoBehaviour
{
    public Transform spawner;
    public Tomato prefab;
    public Transform container;
    public float spawnDelay = 0.5f;
    private readonly List<Tomato> _tomatos = new List<Tomato>();
    public List<TomatoData> tomatoDataList = new List<TomatoData>();
    private Coroutine _spawnRoutine;
    public event Action<Tomato> OnWalk;
 
    public void StartSpawning()
    {
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (_spawnRoutine == null) return;
        StopCoroutine(_spawnRoutine);
        _spawnRoutine = null;
    }
   
    public void StopGame()
    {
        StopSpawning();

        for (var i = _tomatos.Count - 1; i >= 0; i--)
        {
            RemoveTomato(_tomatos[i]);
        }
    }

    private void Spawn()
    {
        var data = tomatoDataList[UnityEngine.Random.Range(0, tomatoDataList.Count)];
        var tomato = Instantiate(prefab, spawner.position, Quaternion.identity);
        tomato.transform.parent = container;
        tomato.Data = data;
        AddTomato(tomato);
    }
 
    private IEnumerator SpawnRoutine()
    {
        Spawn();
        yield return new WaitForSeconds(spawnDelay);
        StartSpawning();
    }

    private void AddTomato(Tomato tomato)
    {
        tomato.OnWalk += TomatoCollectedHandler;
        _tomatos.Add(tomato);
    }

    private void RemoveTomato(Tomato tomato)
    {
        tomato.OnWalk -= TomatoCollectedHandler;
        _tomatos.Remove(tomato);
        Destroy(tomato.gameObject);
    }

    private void TomatoCollectedHandler(Tomato tomato)
    {
        OnWalk?.Invoke(tomato);
        RemoveTomato(tomato);
    }
}