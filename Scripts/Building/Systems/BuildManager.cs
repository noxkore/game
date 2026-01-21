using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HotbarController hotbar;
    [SerializeField] private GenerateWorld world;

    [Header("Preview")]
    [SerializeField] private Color validColor = new Color(1, 1, 1, 0.5f);
    [SerializeField] private Color invalidColor = new Color(1, 0, 0, 0.5f);

    private IGrid grid;

    private GameObject previewInstance;
    private BaseBuildable currentBuildable;
    private ItemData lastItem;

    private int currentRotation = 0; // 0, 90, 180, 270

    private void Start()
    {
        grid = world.GetGrid();
    }

    private void Update()
    {
        UpdateSelectedBuildable();
        grid = world.GetGrid();

        if (currentBuildable == null || grid == null)
        {
            ClearPreview();
            return;
        }

        HandleRotationInput();
        UpdatePreview();

        if (Input.GetMouseButtonDown(0))
            TryPlace();
    }

    private void UpdateSelectedBuildable()
    {
        var slot = hotbar.GetSelectedSlot();
        ItemData currentItem = slot != null ? slot.item : null;

        if (currentItem == lastItem)
            return;

        lastItem = currentItem;
        ClearPreview();
        currentBuildable = null;
        currentRotation = 0;

        if (currentItem == null || currentItem.prefab == null)
            return;

        var buildingInstance = currentItem.prefab.GetComponent<BuildingInstance>();
        if (buildingInstance == null)
            return;

        currentBuildable = buildingInstance.Buildable;
    }

    private void UpdatePreview()
    {
        if (previewInstance == null)
        {
            previewInstance = Instantiate(currentBuildable.Prefab);
            SetPreviewVisual(previewInstance);
        }

        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int cellPos = grid.WorldToCell(mouseWorld);

        if (!grid.IsInside(cellPos))
        {
            previewInstance.SetActive(false);
            return;
        }

        previewInstance.SetActive(true);
        previewInstance.transform.position = grid.CellToWorld(cellPos);
        previewInstance.transform.rotation = Quaternion.Euler(0, 0, currentRotation);

        var cell = grid.GetCell(cellPos) as GridCell;
        if (cell == null)
        {
            SetPreviewColor(invalidColor);
            return;
        }

        bool canPlace = currentBuildable.CanPlace(grid, cell);
        SetPreviewColor(canPlace ? validColor : invalidColor);
    }

    private void HandleRotationInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentRotation += 90;
            if (currentRotation >= 360) currentRotation = 0;

            if (previewInstance != null)
                previewInstance.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
        }
    }

    private void TryPlace()
    {
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int cellPos = grid.WorldToCell(mouseWorld);

        if (!grid.IsInside(cellPos))
            return;

        var cell = grid.GetCell(cellPos) as GridCell;
        if (cell == null)
            return;

        if (!currentBuildable.CanPlace(grid, cell))
            return;

        GameObject obj = Instantiate(
            currentBuildable.Prefab,
            grid.CellToWorld(cellPos),
            Quaternion.Euler(0, 0, currentRotation)
        );

        var instance = obj.GetComponent<BuildingInstance>();
        instance.Initialize(currentBuildable, cell);
        instance.OnPlaced(grid);

        bool hasGetterBrain = obj.GetComponent<GetterBrain>() != null;

        if (!hasGetterBrain)
        {
            for (int x = 0; x < currentBuildable.Size.x; x++)
            {
                for (int y = 0; y < currentBuildable.Size.y; y++)
                {
                    var pos = cell.Position + new Vector2Int(x, y);
                    var c = grid.GetCell(pos);
                    if (c != null)
                        c.Place(currentBuildable);
                }
            }
        }

        var selectedSlot = hotbar.GetSelectedSlot();
        if (selectedSlot != null && !selectedSlot.IsEmpty)
        {
            selectedSlot.amount--;
            if (selectedSlot.amount <= 0)
                selectedSlot.Clear();

            PlayerInventory.Instance.NotifyChanged();
        }
    }

    private void ClearPreview()
    {
        if (previewInstance != null)
            Destroy(previewInstance);

        previewInstance = null;
    }

    private void SetPreviewVisual(GameObject obj)
    {
        foreach (var sr in obj.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = validColor;
            sr.sortingOrder += 1000;
        }
    }

    private void SetPreviewColor(Color color)
    {
        if (previewInstance == null)
            return;

        foreach (var sr in previewInstance.GetComponentsInChildren<SpriteRenderer>())
            sr.color = color;
    }
}