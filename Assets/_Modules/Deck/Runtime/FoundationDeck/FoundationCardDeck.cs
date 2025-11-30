using UnityEngine;

public class FoundationCardDeck : MonoBehaviour
{
    [SerializeField] private GameObject foundationSlotPrefab;
    [SerializeField] private int foundationSlotNumber;
    [SerializeField] private Vector3 foundationSlotPositionGap;
    [SerializeField] private GameObject[] foundationCardSlots;
    private CardSlotFactory cardSlotFactory;

    private void Start()
    {
        InitFoundationCardDeck();
    }

    private void InitFoundationCardDeck()
    {
        this.cardSlotFactory = new CardSlotFactory(this.foundationSlotPositionGap, this.foundationSlotPrefab, this.transform);
        this.foundationCardSlots = this.cardSlotFactory.CreateFoundationCardSlots(this.foundationSlotNumber);
    }
}