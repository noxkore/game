using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D : IGrid
{
    private readonly Dictionary<Vector2Int, IGridCell> cells;
    private readonly int halfWidth;
    private readonly int halfHeight;
    private readonly float cellSize;
    private readonly Vector2 origin;

    public Grid2D(int width, int height, float cellSize, Vector2 origin)
    {
        this.cellSize = cellSize;
        this.origin = origin;

        halfWidth = width / 2;
        halfHeight = height / 2;

        cells = new Dictionary<Vector2Int, IGridCell>();

        for (int x = -halfWidth; x <= halfWidth; x++)
            for (int y = -halfHeight; y <= halfHeight; y++)
            {
                var pos = new Vector2Int(x, y);
                cells[pos] = new GridCell(pos);
            }
    }

    public bool IsInside(Vector2Int position)
    {
        return position.x >= -halfWidth && position.x <= halfWidth
            && position.y >= -halfHeight && position.y <= halfHeight;
    }

    public IGridCell GetCell(Vector2Int position)
    {
        return cells.TryGetValue(position, out var cell) ? cell : null;
    }

    public Vector2Int WorldToCell(Vector2 worldPosition)
    {
        var local = worldPosition - origin;

        return new Vector2Int(
            Mathf.FloorToInt(local.x / cellSize),
            Mathf.FloorToInt(local.y / cellSize)
        );
    }

    public Vector2 CellToWorld(Vector2Int cellPosition)
    {
        return origin + new Vector2(
            cellPosition.x * cellSize + cellSize / 2f,
            cellPosition.y * cellSize + cellSize / 2f
        );
    }
}
