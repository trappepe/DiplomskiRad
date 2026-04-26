using Assets.Scripts.Interaction;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactions / NPC With Action")]
public class NPCInteractionWithAction : NPCInteraction
{
    public InteractionData nextInteraction;
    protected override void TriggerNextInteraction(InteractionContext ctx)
    {
        if(nextInteraction != null)
        {
            nextInteraction.Execute(ctx);
        }
    }
}