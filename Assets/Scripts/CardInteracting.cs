using Lean.Touch;
using UnityEngine;

public class CardInteracting : MonoBehaviour
{
    [SerializeField] private float swapingThreshold;
    [SerializeField] private bool isSelected;
    private Bounds cardBounds;
    private Vector3 fingerOffset;
    private Card[,] cards;
    private Vector3[,] cardPositions;
    public bool IsSelected => this.isSelected;

    public void InitCardInteracting(Card[,] cards, Vector3[,] cardPositions, Bounds bounds)
    {
        this.cards = cards;
        this.cardPositions = cardPositions;
        this.cardBounds = bounds;
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += FingerDownHandler;
        LeanTouch.OnFingerUp += FingerUpHandler;
        LeanTouch.OnFingerUpdate += FingerUpdateHandler;
    }

    private void FingerUpdateHandler(LeanFinger finger)
    {
        if (!IsSelected)
        {
            return;
        }

        Vector3 fingerPos = finger.GetWorldPosition(10);
        this.transform.position = this.fingerOffset + fingerPos;
    }

    private void FingerDownHandler(LeanFinger finger)
    {
        if (!CanSelectCard(finger))
        {
            return;
        }

        this.isSelected = true;

        Vector3 fingerPos = finger.GetWorldPosition(10);
        this.fingerOffset = this.transform.position - fingerPos;
    }

    private void FingerUpHandler(LeanFinger finger)
    {
        this.isSelected = false;
    }

    private void CheckSwappingPosition()
    {
        for (var row = 0; row < this.cardPositions.GetLength(0); row++)
        {
            for (var col = 0; col < this.cardPositions.GetLength(1); col++)
            {
                Vector3 cardPosition = this.cardPositions[row, col];
                float distance = Vector2.Distance(this.transform.position, cardPosition);

                if (distance <= this.swapingThreshold)
                {
                    SwapPosition(row, col);
                    return;
                }
            }
        }
    }

    public void SwapPosition(int row, int col)
    {
        Card card = this.cards[row, col];
    }

    private bool CanSelectCard(LeanFinger finger)
    {
        Vector3 fingerPos = finger.GetWorldPosition(10);

        return this.cardBounds.Contains(fingerPos);
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= FingerDownHandler;
        LeanTouch.OnFingerUp -= FingerUpHandler;
        LeanTouch.OnFingerUpdate -= FingerUpdateHandler;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, this.cardBounds.size);
    }
#endif
}