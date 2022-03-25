using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map", fileName = "Map")]
public class MapInfo : ScriptableObject
{
    [SerializeField] private List<ObjectForSpawn> foodsForSpawn;
    [SerializeField] private List<ObjectForSpawn> enemiesForSpawn;
    [SerializeField] private int maxObjects = 50;

    public List<ObjectForSpawn> FoodsForSpawn { get => foodsForSpawn; }
    public List<ObjectForSpawn> EnemiesForSpawn { get => enemiesForSpawn; }
    public int MaxObjects { get => maxObjects; }
}
