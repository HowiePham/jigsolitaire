using UnityEditor;
using UnityEngine;

public class CardFactory
{
    private bool[,] capturedImageMatrix;
    private int imageWidth;
    private int imageHeight;
    private int row;
    private int col;
    private Texture2D sourceTexture;
    private float pixelsPerUnit;
    private bool random;
    private static string CardPrefabAddress = "Assets/prefabs/Card.prefab";

    public CardFactory(float pixelsPerUnit, int row, int col, Texture2D sourceTexture, bool random)
    {
        this.pixelsPerUnit = pixelsPerUnit;
        this.row = row;
        this.col = col;
        this.sourceTexture = sourceTexture;
        this.random = random;

        this.capturedImageMatrix = new bool[this.row, this.col];
        this.imageWidth = this.sourceTexture.width / this.col;
        this.imageHeight = this.sourceTexture.height / this.row;
    }

    private GameObject GetCardObject(int row, int col, Sprite sprite)
    {
        var cardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CardPrefabAddress);
        var cardGameObject = (GameObject)PrefabUtility.InstantiatePrefab(cardPrefab);
        var card = cardGameObject.GetComponent<Card>();
        cardGameObject.name = $"[{row},{col}]";

        float posY = (row - 1) * (this.imageHeight / this.pixelsPerUnit);
        float posX = (col - 1) * (this.imageWidth / this.pixelsPerUnit);

        card.transform.localPosition = new Vector3(posX, posY, 0);
        card.SetSpriteCardVisual(sprite);
        return cardGameObject;
    }

    private Sprite GetSpriteAtMatrixPos(int row, int col)
    {
        var rect = new Rect(col * this.imageWidth, row * this.imageHeight, this.imageWidth, this.imageHeight);
        var pivot = new Vector2(0.5f, 0.5f);
        var sprite = Sprite.Create(this.sourceTexture, rect, pivot, this.pixelsPerUnit);
        return sprite;
    }

    public Card[,] SpawnAllCardImage()
    {
        if (this.random)
        {
            return SpawnImageRandomly();
        }

        return SpawnImageSequence();
    }

    private Card[,] SpawnImageSequence()
    {
        var cards = new Card[this.row, this.col];

        for (int row = 0; row < this.row; row++)
        {
            for (int col = 0; col < this.col; col++)
            {
                Sprite sprite = GetSpriteAtMatrixPos(row, col);
                GameObject newCardGO = GetCardObject(row, col, sprite);
                this.capturedImageMatrix[row, col] = true;
                cards[row, col] = newCardGO.GetComponent<Card>();
            }
        }

        return cards;
    }

    private Card[,] SpawnImageRandomly()
    {
        var cards = new Card[this.row, this.col];

        for (int row = 0; row < this.row; row++)
        {
            for (int col = 0; col < this.col; col++)
            {
                int imageRow = Random.Range(0, this.row);
                int imageCol = Random.Range(0, this.col);
                bool hasImage = this.capturedImageMatrix[imageRow, imageCol];

                while (hasImage)
                {
                    imageRow = Random.Range(0, this.row);
                    imageCol = Random.Range(0, this.col);
                    hasImage = this.capturedImageMatrix[imageRow, imageCol];
                }

                Sprite sprite = GetSpriteAtMatrixPos(imageRow, imageCol);
                GameObject newCardGO = GetCardObject(row, col, sprite);
                this.capturedImageMatrix[imageRow, imageCol] = true;
                cards[row, col] = newCardGO.GetComponent<Card>();
            }
        }

        return cards;
    }
}