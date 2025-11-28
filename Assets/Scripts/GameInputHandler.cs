using Lean.Touch;
using UnityEngine;
using UnityEngine.Serialization;

public class GameInputHandler : MonoBehaviour
{
    [SerializeField] private ImageCardMatrix cardMatrix;
    [SerializeField] private Card selectedCard;
    [SerializeField] private float swappingThreshold;
    private Vector3 fingerOffset;
    private Card[,] Cards => this.cardMatrix.Cards;
    private Vector3[,] CardPositions => this.cardMatrix.CardPositions;

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += FingerDownHandler;
        LeanTouch.OnFingerUp += FingerUpHandler;
        LeanTouch.OnFingerUpdate += FingerUpdateHandler;
    }

    private void FingerUpdateHandler(LeanFinger finger)
    {
        if (this.selectedCard == null)
        {
            return;
        }

        Vector3 fingerPos = finger.GetWorldPosition(10);
        this.selectedCard.transform.position = this.fingerOffset + fingerPos;
    }

    private void FingerDownHandler(LeanFinger finger)
    {
        Vector3 fingerPos = finger.GetWorldPosition(10);

        foreach (Card card in this.Cards)
        {
            Bounds cardBounds = card.GetBounds();

            if (cardBounds.Contains(fingerPos))
            {
                this.selectedCard = card;
                this.fingerOffset = this.selectedCard.transform.position - fingerPos;
                break;
            }
        }
    }

    private void FingerUpHandler(LeanFinger finger)
    {
        if (this.selectedCard != null)
        {
            CheckSwappingCard();
        }

        this.selectedCard = null;
        this.fingerOffset = Vector3.zero;
    }

    private void CheckSwappingCard()
    {
        int row = this.CardPositions.GetLength(0);
        int col = this.CardPositions.GetLength(1);

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Bounds cardBounds = this.selectedCard.GetBounds();
                Vector3 cardPos = this.CardPositions[i, j];
                float distance = Vector3.Distance(cardBounds.center, cardPos);

                if (distance <= this.swappingThreshold)
                {
                    Debug.Log($"--- (CARD) Move card from [{this.selectedCard.MatrixPos.Row},{this.selectedCard.MatrixPos.Column}] -> [{i},{j}]");
                    SwapCard(i, j);
                    break;
                }
            }
        }
    }

    private void SwapCard(int row, int col)
    {
        MatrixPos selectedCardMatrixPos = this.selectedCard.MatrixPos;
        Card swappedCard = this.Cards[row, col];

        this.selectedCard.SwapToPos(row, col, this.CardPositions[row, col]);
        swappedCard.SwapToPos(selectedCardMatrixPos.Row, selectedCardMatrixPos.Column, this.CardPositions[selectedCardMatrixPos.Row, selectedCardMatrixPos.Column]);

        this.Cards[row, col] = this.selectedCard;
        this.Cards[selectedCardMatrixPos.Row, selectedCardMatrixPos.Column] = swappedCard;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= FingerDownHandler;
        LeanTouch.OnFingerUp -= FingerUpHandler;
        LeanTouch.OnFingerUpdate -= FingerUpdateHandler;
    }
}