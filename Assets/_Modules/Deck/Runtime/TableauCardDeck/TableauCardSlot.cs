using System.Collections.Generic;
using UnityEngine;

public class TableauCardSlot : MonoBehaviour
{
    [SerializeField] private Vector3 cardSlotPositionGap;
    [SerializeField] private Transform cardHolder;
    [SerializeField] private List<Card> tableauCards;

    public void InsertCard(Card card)
    {
        this.tableauCards.Add(card);

        MoveCardToLastPosition(card.transform);
    }

    private void MoveCardToLastPosition(Transform cardTransform)
    {
        cardTransform.SetParent(this.cardHolder);
        int lastIndex = this.tableauCards.Count > 0 ? this.tableauCards.Count - 1 : 0;
        cardTransform.localPosition = this.cardSlotPositionGap * lastIndex;
    }

    public bool CanInsertCard(int cardTypeNumber)
    {
        if (this.tableauCards.Count <= 0)
        {
            return true;
        }

        Card lastTableauCard = this.tableauCards[this.tableauCards.Count - 1];
        return lastTableauCard.CardTypeNumber == cardTypeNumber;
    }
}