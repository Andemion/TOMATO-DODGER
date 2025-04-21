using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [Tooltip("Prefab UI Image du cœur")]
    public GameObject heartPrefab;

    [Tooltip("Nombre de vies actuel du joueur")]
    public int currentLives = 3;

    // Appelé au démarrage (ou chaque fois que la vie change)
    void Start()
    {
        RefreshHearts();
    }

    public void RefreshHearts()
    {
        // 1) Détruire les cœurs existants
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // 2) Instancier un cœur par vie
        for (int i = 0; i < currentLives; i++)
        {
            Instantiate(heartPrefab, transform);
        }
    }
    
    public void SetLives(int lives)
    {
        currentLives = lives;
        RefreshHearts();
    }
}