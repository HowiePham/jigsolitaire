using UnityEngine;

public class ImageCardMatrix : MonoBehaviour
{
    [SerializeField] private Texture2D sourceTexture;
    [SerializeField] private float pixelsPerUnit = 100f;
    [SerializeField] private int row;
    [SerializeField] private int col;
    [SerializeField] private bool random;
    private Card[,] cards;
    private Vector3[,] cardPositions;
    private CardFactory cardFactory;

    public Card[,] Cards => this.cards;
    public Vector3[,] CardPositions => this.cardPositions;

    private void Start()
    {
        SplitImage();
    }

    [ContextMenu("Split")]
    private void SplitImage()
    {
        this.cardFactory = new CardFactory(this.pixelsPerUnit, this.row, this.col, this.sourceTexture, this.random);
        this.cards = this.cardFactory.SpawnAllCardImage();

        this.cardPositions = GetCardPositions();
    }

    private Vector3[,] GetCardPositions()
    {
        var positions = new Vector3[this.row, this.col];
        for (var row = 0; row < this.row; row++)
        {
            for (var col = 0; col < this.row; col++)
            {
                Card card = this.cards[row, col];
                Vector3 cardPos = card.transform.position;
                positions[row, col] = cardPos;
            }
        }

        return positions;
    }
}