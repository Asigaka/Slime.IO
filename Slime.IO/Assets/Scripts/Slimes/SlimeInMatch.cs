using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlimeInMatch
{
    [SerializeField] private string name;
    [SerializeField] private SlimeStomach slimeStomach;
    [SerializeField] private GameObject slimeObject;

    public SlimeInMatch(SlimeStomach slimeStomach, GameObject slimeObject, string name)
    {
        this.slimeStomach = slimeStomach;
        this.slimeObject = slimeObject;
        this.name = name;
    }

    public SlimeStomach SlimeStomach { get => slimeStomach; }
    public GameObject SlimeObject { get => slimeObject; }
    public string Name { get => name; }

    public void ResetSlimeValues()
    {
        slimeObject.SetActive(false);
        slimeStomach.ResetFullness();
        slimeObject.transform.position = SlimesManager.Instance.GetSlimeSpawnPos();
        slimeObject.SetActive(true);
    }
}
