using UnityEngine;
using Assets.Scripts.Interaction;

namespace Assets.Scripts.Interaction
{
    [CreateAssetMenu(menuName = "Interactions / World Change")]
    public class WorldChangeInteraction : InteractionData
    {
        public GameObject objectToDisable;
        public GameObject objectToEnable;

        public override void Execute(InteractionContext ctx)
        {
            if (ctx == null)
                return;

            if (ctx.customDisable != null)
                ctx.customDisable.SetActive(false);

            if (ctx.customEnable != null)
                ctx.customEnable.SetActive(true);

            GameManager.instance.UnlockMovementAndCamera();
        }
    }
}