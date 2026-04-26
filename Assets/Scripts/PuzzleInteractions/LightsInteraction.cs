using Assets.Scripts.Interaction;
using UnityEngine;
[CreateAssetMenu(menuName = "Interactions / Lights")]
public class LightsInteraction : InteractionData
{
    public override void Execute(InteractionContext ctx)
    {
        GameManager.instance.LockMovementAndCamera();
    }
}