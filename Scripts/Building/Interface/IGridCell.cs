using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IGridCell
{
    Vector2Int Position { get; }
    bool IsOccupied { get; }

    IBuildable Occupant { get; }

    bool CanPlace(IBuildable buildable);
    void Place(IBuildable buildable);
    void Clear();
}
