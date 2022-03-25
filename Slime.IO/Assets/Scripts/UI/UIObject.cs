using UnityEngine;

public class UIObject : MonoBehaviour
{
    [SerializeField] private UIType type;

    public UIType Type { get => type; }
}
