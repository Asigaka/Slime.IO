using UnityEngine;

public class FoodObject : MonoBehaviour
{
    [SerializeField] private FoodInfo info;

    public FoodInfo Info { get => info; }

    private void OnTriggerEnter(Collider other)
    {
        SlimeStomach slimeStomach = other.gameObject.GetComponentInChildren<SlimeStomach>();

        if (slimeStomach)
        {
            if (slimeStomach.StomachFullness >= info.StomachFullness)
            {
                slimeStomach.IncreaseStomachFullness(info.FoodValue);
                MapGeneration.Instance.DestroyObject(gameObject);
            }
        }
    }
}
