using Assets.Scripts.Interaction;
using UnityEngine;
using Assets.Scripts.Core;

public abstract class InteractionData : ScriptableObject
{
    public string Prompt;
    
     [Header("Item Logic")]
    public bool requiresItem;
    public ItemTypes requiredItem;
    public bool givesItem;
    public ItemTypes rewardItem;
    
    public abstract void Execute(InteractionContext ctx);
}
