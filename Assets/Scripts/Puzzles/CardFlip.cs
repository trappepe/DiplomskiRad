using UnityEngine;

public class CardFlip : MonoBehaviour
{
    private Card card;

    void Awake()
    {
        card = GetComponent<Card>();
    }

    void OnMouseDown()
    {
        if (card != null)
            card.Flip();
    }
}
