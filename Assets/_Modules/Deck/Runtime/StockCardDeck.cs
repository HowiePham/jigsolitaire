using System;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class StockCardDeck : MonoBehaviour
{
    [Header("Stock Cards")] [SerializeField]
    private SpriteRenderer stockCardVisual;

    [SerializeField] private Transform stockCardsHolder;
    [SerializeField] private List<CardInformation> stockCards = new List<CardInformation>();
    private Stack<CardInformation> stockCardStack = new Stack<CardInformation>();
    private Bounds Bounds => this.stockCardVisual.bounds;

    [Header("Waste Cards")] [SerializeField]
    private int maxWasteCard;

    [SerializeField] private int wasteCardIndex;
    [SerializeField] private Transform wasteCardsHolder;
    [SerializeField] private Vector3 wasteCardPositionGap;
    [SerializeField] private List<Card> wasteCards = new List<Card>();
    [SerializeField] private Card cardPrefab;
    private Stack<CardInformation> wasteCardStack = new Stack<CardInformation>();
    private CardFactory cardFactory;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        LeanTouch.OnFingerDown += FingerDownHandler;

        InitStockCard();
        InitWasteCard();
    }

    private void InitStockCard()
    {
        int stockCardsCount = this.stockCards.Count;
        for (int i = stockCardsCount - 1; i >= 0; i--)
        {
            this.stockCardStack.Push(this.stockCards[i]);
        }
    }

    private void InitWasteCard()
    {
        this.cardFactory = new CardFactory(this.cardPrefab, this.wasteCardsHolder);
        for (int i = 0; i < this.maxWasteCard; i++)
        {
            Vector3 cardPos = this.wasteCardPositionGap * i;
            Card newWasteCard = this.cardFactory.CreateCard(cardPos);
            newWasteCard.gameObject.SetActive(false);

            this.wasteCards.Add(newWasteCard);
        }
    }

    private void FingerDownHandler(LeanFinger finger)
    {
        Vector3 fingerPos = finger.GetWorldPosition(10);

        if (!Bounds.Contains(fingerPos))
        {
            return;
        }

        GetNextStockCard();
    }

    private void GetNextStockCard()
    {
        if (this.stockCardStack.Count <= 0)
        {
            ResetStockCards();
            return;
        }

        CardInformation nextCardInformation = this.stockCardStack.Pop();
        this.wasteCardStack.Push(nextCardInformation);

        ShowWasteCard(nextCardInformation);

        if (this.stockCardStack.Count <= 0)
        {
            SetActiveStockCardVisual(false);
        }
    }

    private void ShowWasteCard(CardInformation cardInformation)
    {
        if (this.wasteCardIndex < this.maxWasteCard)
        {
            //Hien thi waste Card tiep theo
            this.wasteCards[this.wasteCardIndex].gameObject.SetActive(true);
            this.wasteCardIndex++;
        }
        else
        {
            //dich lan luot waste card (1->0, 2->1)
            //Hien thi waste card index la 3
        }
    }

    private void ResetStockCards()
    {
        while (this.wasteCardStack.Count > 0)
        {
            CardInformation cardInformation = this.wasteCardStack.Pop();
            this.stockCardStack.Push(cardInformation);
        }

        ResetWasteCards();
        SetActiveStockCardVisual(true);
    }

    private void ResetWasteCards()
    {
        foreach (Card wasteCard in this.wasteCards)
        {
            wasteCard.gameObject.SetActive(false);
        }

        this.wasteCardIndex = 0;
    }

    private void SetActiveStockCardVisual(bool enable)
    {
        this.stockCardVisual.enabled = enable;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= FingerDownHandler;
    }
}