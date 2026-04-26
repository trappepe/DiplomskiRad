using UnityEngine;

public class Frame : MonoBehaviour
{
     public int currentIndex;
    public int correctIndex;

    private FrameManager puzzle;

    public void SetPuzzle(FrameManager p)
    {
        puzzle = p;
    }
}
