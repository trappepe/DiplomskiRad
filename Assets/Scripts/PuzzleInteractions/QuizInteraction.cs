using Assets.Scripts.Interaction;
using UnityEngine;
 [CreateAssetMenu(menuName = "Interactions / Quiz")]
 public class QuizInteraction : InteractionData
 {
    public override void Execute(InteractionContext ctx)
    {
        UIManager.instance.ShowPanel();
        InteractionActions.StartQuiz(ctx);
        GameManager.instance.LockMovementAndCamera();
    }

    public void CloseQuiz()
    {
        UIManager.instance.HidePanel();
        GameManager.instance.UnlockMovementAndCamera();
    }
}