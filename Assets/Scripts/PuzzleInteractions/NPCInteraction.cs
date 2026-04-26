using Assets.Scripts.Interaction;
using UnityEngine;
[CreateAssetMenu(menuName = "Interactions / NPC")]
public class NPCInteraction : InteractionData
{
    public NPCData npcData;

    public override void Execute(InteractionContext ctx)
    {
        var gm = ctx.gameManager;
        Animator anim = ctx.target.GetComponent<Animator>();
        if (anim != null && !string.IsNullOrEmpty(npcData.talkTrigger))
        {
            anim.SetTrigger(npcData.talkTrigger);
        }
        GameManager.instance.LockMovementAndCamera();
        if (gm.puzzleFinished && npcData.blocksAfterFinish)
        {
            UIManager.instance.ShowDialogue(npcData.finishedText);
            return;
        }
        if (!requiresItem)
        {
            if (gm.finishedFrame == true)
            {
                UIManager.instance.ShowChoice(npcData.hasItemText, () =>
                {
                    TriggerNextInteraction(ctx);
                });
                return;
            }
            UIManager.instance.ShowDialogue(npcData.normalText);
            return;
        }
        if (requiresItem && !gm.HasItem(requiredItem))
        {
            UIManager.instance.ShowDialogue(npcData.noItemText);
            return;
        }
        UIManager.instance.ShowChoice(npcData.hasItemText, () =>
        {
            if (requiresItem)
            {
                gm.ConsumeItem();
            }
            TriggerNextInteraction(ctx);
        });
    }

    protected virtual void TriggerNextInteraction(InteractionContext ctx)
    {
        Debug.Log("No interaction assigned");
    }
}