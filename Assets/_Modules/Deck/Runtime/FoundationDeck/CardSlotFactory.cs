using UnityEditor;
using UnityEngine;

public class CardSlotFactory
{
    private Transform parent;
    private Vector3 cardSlotPositionGap;
    private GameObject cardSlotPrefab;

    public CardSlotFactory(Vector3 cardSlotPositionGap, GameObject cardSlotPrefab, Transform parent)
    {
        this.cardSlotPositionGap = cardSlotPositionGap;
        this.cardSlotPrefab = cardSlotPrefab;
        this.parent = parent;
    }

    public GameObject[] CreateFoundationCardSlots(int foundationSlotNumber)
    {
        var foundationCardSlots = new GameObject[foundationSlotNumber];

        for (int i = 0; i < foundationSlotNumber; i++)
        {
            var foundationSlot = (GameObject)PrefabUtility.InstantiatePrefab(this.cardSlotPrefab, this.parent);
            foundationSlot.transform.localPosition = this.cardSlotPositionGap * i;

            foundationCardSlots[i] = foundationSlot;
        }

        return foundationCardSlots;
    }
}