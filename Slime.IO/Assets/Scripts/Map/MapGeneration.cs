using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private MapInfo generationMap;
    [SerializeField] private Transform plane;
    [SerializeField] private List<GameObject> spawnedObjects;

    public static MapGeneration Instance;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        spawnedObjects = new List<GameObject>();

        foreach (ObjectForSpawn mapObject in generationMap.FoodsForSpawn)
        {
            int amount = Random.Range(mapObject.MinAmount, mapObject.MaxAmount);

            for (int i = 0; i < amount; i++)
            {
                SpawnObjectOnMap(mapObject.SpawnedObject);
            }
        }

        CheckAndFillObjects();
    }

    private void CheckAndFillObjects()
    {
        if (spawnedObjects.Count < generationMap.MaxObjects)
        {
            for (int i = spawnedObjects.Count; i < generationMap.MaxObjects; i++)
            {
                GameObject randomObject = generationMap.FoodsForSpawn[Random.Range(0, generationMap.FoodsForSpawn.Count)].SpawnedObject;
                SpawnObjectOnMap(randomObject);
            }
        }
    }

    public void DestroyObject(GameObject destroyObject)
    {
        if (spawnedObjects.Contains(destroyObject))
        {
            spawnedObjects.Remove(destroyObject);
            Destroy(destroyObject);
        }

        CheckAndFillObjects();
    }

    private void SpawnObjectOnMap(GameObject spawnObject)
    {
        Vector3 objPos = GetRandomPosOnMap();

        GameObject newObject = Instantiate(spawnObject);
        newObject.transform.position = objPos;
        newObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        newObject.transform.SetParent(plane.transform);

        spawnedObjects.Add(newObject);
    }

    public Vector3 GetRandomPosOnMap()
    {
        return new Vector3(Random.Range(GetMinPlaneScale(), GetMaxPlaneScale()),
                    0, Random.Range(GetMinPlaneScale(), GetMaxPlaneScale()));
    }

    private float GetMinPlaneScale()
    {
        return plane.transform.localScale.x * -5;
    }

    private float GetMaxPlaneScale()
    {
        return plane.transform.localScale.x * 5;
    }
}
