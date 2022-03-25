using UnityEngine;

[System.Serializable]
public class ObjectForSpawn
{
    [SerializeField] private GameObject spawnedObject;
    [SerializeField] private int minAmount;
    [SerializeField] private int maxAmount;

    public GameObject SpawnedObject { get => spawnedObject; }
    public int MinAmount { get => minAmount; }
    public int MaxAmount { get => maxAmount; }
}
