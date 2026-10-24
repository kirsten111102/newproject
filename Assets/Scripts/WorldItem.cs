using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField]
    private string uniqueId;
    public string UniqueId
    {
        get => uniqueId;
        set => uniqueId = value;
    }
}
