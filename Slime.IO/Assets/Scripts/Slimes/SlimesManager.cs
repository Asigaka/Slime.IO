using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlimesManager : MonoBehaviour
{
    [SerializeField] private int maxSlimes = 10;
    [SerializeField] private List<SlimeInMatch> slimesInMatch;
    [SerializeField] private List<GameObject> slimesPrefabs;
    [SerializeField] private GameObject playerPrefab;

    public static SlimesManager Instance;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        FillMatch();
    }

    private void FillMatch()
    {
        slimesInMatch = new List<SlimeInMatch>();

        SpawnNewSlime(playerPrefab, GetSlimeSpawnPos());

        for (int i = 0; i < maxSlimes; i++)
        {
            SpawnNewSlime(slimesPrefabs[Random.Range(0, slimesPrefabs.Count)], GetSlimeSpawnPos());
        }
    }

    private void SpawnNewSlime(GameObject slimePrefab, Vector3 spawnPos)
    {
        GameObject newSlime = Instantiate(slimePrefab, spawnPos, Quaternion.identity);
        SlimeInMatch slimeInMatch = new SlimeInMatch(newSlime.GetComponent<SlimeStomach>(), newSlime, "Slime" + slimesInMatch.Count);
        slimesInMatch.Add(slimeInMatch);
    }

    public void RespawnSlime(SlimeStomach stomach)
    {
        SlimeInMatch slime = GetSlimeByStomach(stomach);
        slime.ResetSlimeValues();
    }

    private SlimeInMatch GetSlimeByStomach(SlimeStomach stomach)
    {
        foreach (SlimeInMatch slime in slimesInMatch)
        {
            if (slime.SlimeStomach == stomach)
            {
                return slime;
            }
        }

        return null;
    }

    public Vector3 GetSlimeSpawnPos()
    {
        Vector3 randomPosOnMap = MapGeneration.Instance.GetRandomPosOnMap();
        return new Vector3(randomPosOnMap.x, 2, randomPosOnMap.z);
    }

    public void UpdateLeaderboard()
    {
        slimesInMatch.Sort(delegate (SlimeInMatch t1, SlimeInMatch t2)
        {
            return t2.SlimeStomach.StomachFullness.CompareTo(t1.SlimeStomach.StomachFullness);
        });

        SlimeInMatch firstLeader = slimesInMatch[0];
        SlimeInMatch secondLeader = slimesInMatch[1];
        SlimeInMatch thirdLeader = slimesInMatch[2];

        UIMatch.Instance.UpdateLeaderboard(firstLeader.Name, firstLeader.SlimeStomach.StomachFullness.ToString(),
            secondLeader.Name, secondLeader.SlimeStomach.StomachFullness.ToString(),
            thirdLeader.Name, thirdLeader.SlimeStomach.StomachFullness.ToString());
    }
}
