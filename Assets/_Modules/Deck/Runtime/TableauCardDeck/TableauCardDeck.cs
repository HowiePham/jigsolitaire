using UnityEngine;

public class TableauCardDeck : MonoBehaviour
{
    [SerializeField] private GameObject tableauSlotPrefab;
    [SerializeField] private int maxTableauSlot;
    [SerializeField] private Vector3 tableauSlotPositionGap;
    [SerializeField] private GameObject[] tableauSlots;
    private CardSlotFactory cardSlotFactory;

    private void Start()
    {
        InitFoundationCardDeck();
    }

    private void InitFoundationCardDeck()
    {
        this.cardSlotFactory = new CardSlotFactory(this.tableauSlotPositionGap, this.tableauSlotPrefab, this.transform);
        this.tableauSlots = this.cardSlotFactory.CreateFoundationCardSlots(this.maxTableauSlot);
    }
}