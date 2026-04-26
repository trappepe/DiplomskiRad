using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardID;
    private MemoryPuzzle puzzle;

    private MeshRenderer meshRenderer;
    private Material backMaterial;
    private Material frontMaterial;
    private bool flipped = false;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetPuzzle(MemoryPuzzle p)
    {
        puzzle = p;
    }

    public void SetBack(Material back)
    {
        backMaterial = back;
    }

    public void SetFront(Material front)
    {
        frontMaterial = front;
    }

    public void Flip()
    {
        if (flipped) return;
        flipped = true;
        meshRenderer.material = frontMaterial;
        puzzle.CardFlipped(this);
    }

    public void Hide()
    {
        flipped = false;
        meshRenderer.material = backMaterial;
    }
}
