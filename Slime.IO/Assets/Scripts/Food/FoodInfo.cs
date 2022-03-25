using UnityEngine;

[CreateAssetMenu(menuName = "Food", fileName = "Food")]
public class FoodInfo : ScriptableObject
{
    [SerializeField] private float stomachFullness;
    [SerializeField] private float foodValue;

    public float StomachFullness { get => stomachFullness; }
    public float FoodValue { get => foodValue; }
}
