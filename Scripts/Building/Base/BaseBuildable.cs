using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building/Base Buildable")]
public class BaseBuildable : ScriptableObject, IBuildable
{
    [Header("Identity")]
    [SerializeField] private string id;

    [Header("Grid")]
    [SerializeField] private Vector2Int size = Vector2Int.one;

    [Header("Visual")]
    [SerializeField] private GameObject prefab;

    public string Id => id;
    public Vector2Int Size => size;
    public GameObject Prefab => prefab;

    public bool CanPlace(IGrid grid, GridCell originCell)
    {
        if (grid == null || originCell == null)
            return false;

        for (int x = 0; x < size.x; x++)
            for (int y = 0; y < size.y; y++)
            {
                var pos = originCell.Position + new Vector2Int(x, y);

                if (!grid.IsInside(pos))
                    return false;

                var cell = grid.GetCell(pos);
                if (cell == null || cell.IsOccupied)
                    return false;
            }

        return true;
    }
}