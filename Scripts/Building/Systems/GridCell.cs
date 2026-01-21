using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : IGridCell
{
    public Vector2Int Position { get; }
    public bool IsOccupied => Occupant != null;
    public IBuildable Occupant { get; private set; }

    public GridCell(Vector2Int position)
    {
        Position = position;
    }

    public bool CanPlace(IBuildable buildable)
    {
        return !IsOccupied;
    }

    public void Place(IBuildable buildable)
    {
        Occupant = buildable;
    }

    public void Clear()
    {
        Occupant = null;
    }
}
