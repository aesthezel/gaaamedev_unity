using UnityEngine;

[CreateAssetMenu(fileName = "Racer", menuName = "Examples/Random Stuff/Racer", order = 0)]
public class Racer : ScriptableObject 
{
    public string RacerName;
    public float RacerSpeed;
    public Color RacerColor;
    private Transform position;

    public void AttachTransform (Transform transform) => position = transform;
    public Color ChangeColor (Color color) => color;
}