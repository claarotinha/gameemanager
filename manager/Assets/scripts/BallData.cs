using UnityEngine;

[CreateAssetMenu(fileName = "New Ball", menuName = "Bolinha/Ball Data")]
public class BallData : ScriptableObject
{
    public string ballName;

    public Sprite sprite;

    [Header("Status")]
    public float speed = 5f;
    public float pushForce = 12f;
    public float weight = 1f;
    public float size = 1f;

    [Header("Cor")]
    public Color player1Color;
    public Color player2Color;
}