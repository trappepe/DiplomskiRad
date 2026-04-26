using Assets.Scripts.Interaction;
using UnityEngine;
using System.Collections;

 [CreateAssetMenu(menuName = "Interactions / Door")]
public class DoorInteraction : InteractionData
{
    public float delayBeforeLoad = 1.1f;
        public int targetSceneIndex;

    public override void Execute(InteractionContext ctx)
    {
        if (requiresItem && !ctx.gameManager.HasItem(requiredItem))
        {
            GameManager.instance.LockMovementAndCamera();
            UIManager.instance.ShowDialogue("Потребан ти је кључ");
            return;
        }
        AudioController.instance.PlayDoor();
        ctx.gameManager.StartCoroutine(DelayedLoad());
    }

    private IEnumerator DelayedLoad()
    {
        yield return new WaitForSeconds(delayBeforeLoad);

        GameManager.instance.NextLevel(targetSceneIndex);
    }
}