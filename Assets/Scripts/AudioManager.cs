using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip mainTheme;
    public AudioSource audioSource;

    public void StartGame()
    {
        if(mainTheme != null){
            audioSource.clip = mainTheme;
            audioSource.Play();
        }
    }

    public void StopGame()
    {
        audioSource.Stop();
    }
}
