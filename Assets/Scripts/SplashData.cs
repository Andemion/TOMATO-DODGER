using UnityEngine;

[CreateAssetMenu(fileName = "SplashData", menuName = "SplashData", order = 0)]
public class SplashData : ScriptableObject
{
    [Tooltip("Prefab à instancier quand la tomate s'écrase")]
    public GameObject smashPrefab;
    public Sprite image;
    public int score;
    public int life;
    public float timeEffect;
    public AudioClip smashSfx;
}
