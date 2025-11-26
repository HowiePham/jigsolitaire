using UnityEngine;

public class ImageMatrix : MonoBehaviour
{
    [SerializeField] private Texture2D sourceTexture;
    [SerializeField] private float pixelsPerUnit = 100f;
    [SerializeField] private int row;
    [SerializeField] private int col;
    [SerializeField] private bool random;
    private Card[,] cards;
    private CardFactory cardFactory;
    public Card[,] Cards => this.cards;

    [ContextMenu("Split")]
    private void SplitImage()
    {
        this.cardFactory = new CardFactory(this.pixelsPerUnit, this.row, this.col, this.sourceTexture, this.random);
        this.cards = this.cardFactory.SpawnAllCardImage();
    }
}