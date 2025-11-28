using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardVisual cardVisual;
    [SerializeField] private MatrixPos matrixPos;

    public MatrixPos MatrixPos => this.matrixPos;

    public void InitCard(MatrixPos matrixPos)
    {
        this.matrixPos = matrixPos;
    }

    public void SwapToPos(int row, int col, Vector3 pos)
    {
        this.matrixPos.Row = row;
        this.matrixPos.Column = col;
        this.transform.position = pos;
    }

    public void SetSpriteCardVisual(Sprite sprite)
    {
        this.cardVisual.SetSpriteVisual(sprite);
    }

    public Bounds GetBounds()
    {
        return this.cardVisual.GetBounds();
    }
}