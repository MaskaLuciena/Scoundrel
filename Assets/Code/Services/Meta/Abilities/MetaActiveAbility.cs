using UnityEngine;

[CreateAssetMenu(fileName = "MetaActiveAbility", menuName = "Meta/Active Ability")]
public class MetaActiveAbility : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private string name;
    [SerializeField] private string description; 
    [SerializeField] private int cost;
    [SerializeField] private Sprite icon;

    public string Id => id;
    public string Name => name;
    public string Description => description;
    public int Cost => cost;
    public Sprite Icon => icon;
}