using UnityEngine;

public class FoundationCardSlot : MonoBehaviour
{
    [SerializeField] private int cardType;
    [SerializeField] private bool hasFoundationCard;
    private const int InvalidCardType = -1;

    public Vector3 Position => this.transform.position;
    public int CardType => this.cardType;
    public bool HasFoundationCard => this.hasFoundationCard;

    public void InsertCard(int cardType)
    {
        this.cardType = cardType;
        this.hasFoundationCard = true;
    }

    public void FinishCard()
    {
        this.cardType = InvalidCardType;
        this.hasFoundationCard = false;
    }
}