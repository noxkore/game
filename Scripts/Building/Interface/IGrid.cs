using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrid
{
    Vector2Int WorldToCell(Vector2 worldPosition);
    Vector2 CellToWorld(Vector2Int cellPosition);

    IGridCell GetCell(Vector2Int position);
    bool IsInside(Vector2Int position);
}
