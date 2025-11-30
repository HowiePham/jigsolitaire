using UnityEditor;
using UnityEngine;

public class FoundationCardSlotFactory
{
    private Transform parent;
    private Vector3 foundationSlotPositionGap;
    private FoundationCardSlot foundationCardSlotPrefab;

    public FoundationCardSlotFactory(Vector3 foundationSlotPositionGap, FoundationCardSlot foundationCardSlotPrefab, Transform parent)
    {
        this.foundationSlotPositionGap = foundationSlotPositionGap;
        this.foundationCardSlotPrefab = foundationCardSlotPrefab;
        this.parent = parent;
    }

    public FoundationCardSlot[] CreateFoundationCardSlots(int foundationSlotNumber)
    {
        var foundationCardSlots = new FoundationCardSlot[foundationSlotNumber];

        for (int i = 0; i < foundationSlotNumber; i++)
        {
            var foundationSlot = (FoundationCardSlot)PrefabUtility.InstantiatePrefab(this.foundationCardSlotPrefab, this.parent);
            foundationSlot.transform.localPosition = this.foundationSlotPositionGap * i;

            foundationCardSlots[i] = foundationSlot;
        }

        return foundationCardSlots;
    }
}