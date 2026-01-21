using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInstance : MonoBehaviour, IBuilding
{
    [SerializeField] private BaseBuildable buildable;

    public BaseBuildable Buildable => buildable;
    public GridCell OriginCell { get; private set; }
    public GameObject GameObject => gameObject;

    public void Initialize(BaseBuildable buildable, GridCell originCell)
    {
        this.buildable = buildable;
        OriginCell = originCell;
    }

    public void OnPlaced(IGrid grid)
    {

    }

    public void OnRemoved(IGrid grid)
    {
        Destroy(gameObject);
    }
}
