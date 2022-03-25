using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType { MainMenu, Match, Shop, Settings}
public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIObject> uiObjectsList;
    [SerializeField] private UIType startType;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        Toogle(startType);
    }

    public void Toogle(UIType type)
    {
        foreach (UIObject ui in uiObjectsList)
        {
            ui.gameObject.SetActive(ui.Type == type);
        }
    }
}
