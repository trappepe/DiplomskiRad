
using Assets.Scripts.Interaction;
using UnityEngine;
 [CreateAssetMenu(menuName = "Interactions / Puzzle")]
public class PuzzleInteraction : InteractionData
{
    public override void Execute(InteractionContext ctx)
    {
        InteractionActions.StartMemoryPuzzle(ctx);
    }
}
