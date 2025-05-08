using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }  // Singleton
    public AudioClip mainTheme;
    public AudioSource audioSource;

    private void Awake()
    {
        // Si une instance existe déjà, on détruit ce GameObject
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Sinon, on garde l'instance
        Instance = this;

        // Ne pas détruire cet objet quand on change de scène
        DontDestroyOnLoad(gameObject);
    }
    
    public void StartMainTheme()
    {
        if(mainTheme != null){
            audioSource.clip = mainTheme;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
