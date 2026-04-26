using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Core;

public class MemoryPuzzle : MonoBehaviour
{
    [Header("Cards and Materials")]
    public List<Card> cards; 
    public Material cardBack;
    public Material[] cardFronts; 

    private Card firstCard;
    private Card secondCard;
    private bool isActive = false;
    private bool isChecking = false;
    private int pairsMatched = 0;

    void Update()
    {
        if (!isActive) 
            return;

        int layerMask = LayerMask.GetMask("Cards");

        if (Input.GetMouseButtonDown(0) && (firstCard == null || secondCard == null))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500f, layerMask))
            {
                Debug.Log("Ray hit: " + hit.collider.name);
                Card clickedCard = hit.collider.GetComponent<Card>();
                if (clickedCard != null)
                {
                    clickedCard.Flip();
                }
            }
        }
    }

    public void ActivatePuzzle()
    {
        if (isActive) 
            return;

        isActive = true;
        pairsMatched = 0;
        firstCard = null;
        secondCard = null;
        isChecking = false;

        AssignPairsRandomly();
        ResetAllCards();
    }

    private void AssignPairsRandomly()
    {
        List<int> pairIDs = new List<int> { 0, 0, 1, 1, 2, 2 };

        for (int i = 0; i < pairIDs.Count; i++)
        {
            int rand = Random.Range(i, pairIDs.Count);
            int temp = pairIDs[i];
            pairIDs[i] = pairIDs[rand];
            pairIDs[rand] = temp;
        }

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].cardID = pairIDs[i];
            cards[i].SetPuzzle(this);
            cards[i].SetBack(cardBack);
            cards[i].SetFront(cardFronts[pairIDs[i]]);
        }
    }

    private void ResetAllCards()
    {
        foreach (Card c in cards)
        {
            c.Hide();
        }
    }

    public void CardFlipped(Card card)
    {
        if (!isActive || isChecking) 
            return;

        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        isChecking = true;

        yield return new WaitForSeconds(0.5f);

        if (firstCard.cardID == secondCard.cardID)
        {
            pairsMatched++;
            if (pairsMatched == cardFronts.Length)
                PuzzleFinished();
        }
        else
        {
            firstCard.Hide();
            secondCard.Hide();
        }

        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

    private void PuzzleFinished()
    {
        isActive = false;
        GameManager.instance.puzzleFinished = true;
        GameManager.instance.focusCamera.StopFocus();
        pairsMatched = 0;
        GameManager.instance.ConsumeItem();
        GameManager.instance.GiveItem(ItemTypes.Keys);
        Debug.Log("Keys aquired");
    }
    
}
