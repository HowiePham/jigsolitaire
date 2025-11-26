using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardVisual cardVisual;

    public void SetSpriteCardVisual(Sprite sprite)
    {
        this.cardVisual.SetSpriteVisual(sprite);
    }

    public Bounds GetBounds()
    {
        return this.cardVisual.GetBounds();
    }
}