using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildable
{
    string Id { get; }
    Vector2Int Size { get; }
    GameObject Prefab { get; }

    bool CanPlace(IGrid grid, GridCell originCell);
}