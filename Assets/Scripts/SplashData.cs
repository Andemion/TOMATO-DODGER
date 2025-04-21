using UnityEngine;

[CreateAssetMenu(fileName = "SplashData", menuName = "SplashData", order = 0)]
public class SplashData : ScriptableObject
{
    [Tooltip("Prefab à instancier quand la tomate s'écrase")]
    public Sprite image;
    public int score;
    public int lifeDelta;
    public AudioClip smashSfx;
    public float freezeDuration = 0f;
    public float slideSpeed = 0f;
    public float slideDuration = 0f;
    public bool teleportsPlayer = false;
    
}
