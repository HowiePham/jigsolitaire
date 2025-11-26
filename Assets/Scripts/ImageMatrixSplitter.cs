using UnityEngine;

public class ImageMatrixSplitter : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Texture2D sourceTexture;
    [SerializeField] private float pixelsPerUnit = 100f;
    [SerializeField] private int row;
    [SerializeField] private int col;
    [SerializeField] private bool random;
    private bool[,] capturedImageMatrix;
    private int imageWidth;
    private int imageHeight;

    [ContextMenu("Split")]
    private void SplitImage()
    {
        this.capturedImageMatrix = new bool[this.row, this.col];

        this.imageWidth = this.sourceTexture.width / this.col;
        this.imageHeight = this.sourceTexture.height / this.row;

        if (this.random)
        {
            SpawnImageRandomly();
        }
        else
        {
            SpawnImageSequence();
        }
    }

    private void SpawnImageSequence()
    {
        for (int row = 0; row < this.row; row++)
        {
            for (int col = 0; col < this.col; col++)
            {
                Sprite sprite = GetSpriteAtMatrixPos(row, col);
                GameObject newImageGO = GetImageObject(row, col, sprite);
                this.capturedImageMatrix[row, col] = true;
            }
        }
    }

    private void SpawnImageRandomly()
    {
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
                GameObject newImageGO = GetImageObject(row, col, sprite);
                this.capturedImageMatrix[imageRow, imageCol] = true;
            }
        }
    }

    private GameObject GetImageObject(int row, int col, Sprite sprite)
    {
        Card card = Instantiate(this.cardPrefab);
        GameObject cardGameObject = card.gameObject;
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
}