using UnityEngine;

public class FoundationCardDeck : MonoBehaviour
{
    [SerializeField] private FoundationCardSlot foundationSlotPrefab;
    [SerializeField] private int foundationSlotNumber;
    [SerializeField] private Vector3 foundationSlotPositionGap;
    [SerializeField] private FoundationCardSlot[] foundationCardSlots;
    private FoundationCardSlotFactory foundationCardSlotFactory;

    private void Start()
    {
        InitFoundationCardDeck();
    }

    private void InitFoundationCardDeck()
    {
        this.foundationCardSlotFactory = new FoundationCardSlotFactory(this.foundationSlotPositionGap, this.foundationSlotPrefab, this.transform);
        this.foundationCardSlots = this.foundationCardSlotFactory.CreateFoundationCardSlots(this.foundationSlotNumber);
    }
}