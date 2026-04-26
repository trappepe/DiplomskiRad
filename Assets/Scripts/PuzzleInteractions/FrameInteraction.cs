using Assets.Scripts.Interaction;
using UnityEngine;
[CreateAssetMenu(menuName = "Interactions / Frame")]
public class FrameInteraction : InteractionData
{
    public override void Execute(InteractionContext ctx)
    {
        InteractionActions.StartFramePuzzle(ctx);
    }
}