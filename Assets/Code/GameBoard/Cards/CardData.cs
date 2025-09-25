using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Card")]
public class CardData : ScriptableObject
{
    public string Id;
    public string cardName;
    public CardType type;
    public int value;
    public Sprite icon;
}