using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardVisual cardVisual;
    [SerializeField] private CardInformation cardInformation;

    public void InitCard(CardInformation cardInformation, Sprite cardSprite)
    {
        this.cardInformation = cardInformation;
        this.cardVisual.SetSpriteVisual(cardSprite);
    }

    public Bounds GetBounds()
    {
        return this.cardVisual.GetBounds();
    }
}