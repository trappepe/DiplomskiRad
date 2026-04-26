using Assets.Scripts.Interaction;
using UnityEngine;

 [CreateAssetMenu(menuName = "Interactions / Book")]
 public class BookInteraction : InteractionData
{
    private InteractionContext ctx;
    public InteractionData nextInteraction;
    public override void Execute(InteractionContext ctx)
    {
        this.ctx = ctx;
        UIManager.instance.ShowPanel();
        GameManager.instance.LockMovementAndCamera();
    }

    public void CloseBook()
    {
        UIManager.instance.HidePanel();
        GameManager.instance.UnlockMovementAndCamera();
    }

    public void PlayFrameGame()
    {
        UIManager.instance.HidePanel();
        nextInteraction.Execute(ctx);
    }
}