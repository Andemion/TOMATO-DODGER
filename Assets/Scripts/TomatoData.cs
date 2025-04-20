using UnityEngine;

[CreateAssetMenu(fileName = "TomatoData", menuName = "TomatoData", order = 0)]
public class TomatoData : ScriptableObject
{
    public Sprite image;
    public int life;
    public float launchForce;
    public float probability;
    public AudioClip smashSfx;
    public SplashData splashData;
}
