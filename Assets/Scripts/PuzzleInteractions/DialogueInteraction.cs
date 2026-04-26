using Assets.Scripts.Interaction;
using UnityEngine;
 [CreateAssetMenu(menuName = "Interactions / Dialogue")]
 public class DialogueInteraction : InteractionData
{
    public string dialogue;
    public override void Execute(InteractionContext ctx)
    {
        UIManager.instance.ShowDialogue(dialogue);
    }
}