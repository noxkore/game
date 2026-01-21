using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    BaseBuildable Buildable { get; }
    GridCell OriginCell { get; }
    GameObject GameObject { get; }

    void OnPlaced(IGrid grid);
    void OnRemoved(IGrid grid);
}