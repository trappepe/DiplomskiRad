using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Interaction
{
    public static class InteractionActions
    {
        public static void StartMemoryPuzzle(InteractionContext ctx)
        {
            var puzzle = ctx.target.GetComponent<MemoryPuzzle>();
            if (puzzle == null)
                return;

            GameManager.instance.focusCamera.StartFocus(ctx.focusTransform, ctx.focusRotation);
            puzzle.ActivatePuzzle();
        }

        public static void StartQuiz(InteractionContext ctx)
        {
            var quiz = ctx.target.GetComponent<QuizGame>();
            if (quiz == null)
                return;

            GameManager.instance.focusCamera.StartFocus(ctx.focusTransform, ctx.focusRotation);
        }

        public static void StartFramePuzzle(InteractionContext ctx)
        {
            var framePuzzle = ctx.target.GetComponent<FrameManager>();
            if (framePuzzle == null)
                return;

            GameManager.instance.focusCamera.StartFocus(ctx.focusTransform, ctx.focusRotation);
            framePuzzle.ActivatePuzzle();
        }
    }
}
