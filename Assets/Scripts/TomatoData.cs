using UnityEngine;

[CreateAssetMenu(fileName = "TomatoData", menuName = "TomatoData", order = 0)]
public class TomatoData : ScriptableObject
{
    public Sprite image;
    public int score;
    public float speed;
    public int life;
    public float launchForce;
}
