using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    [Header("NavMesh")]
    [SerializeField] private BakeNavMesh2D bakeNavMesh2D;

    [Header("World Size")]
    [SerializeField] private int width = 9;
    [SerializeField] private int height = 9;
    [SerializeField] private float cellSize = 1f;

    [Header("Terrain Noise")]
    [SerializeField] private float terrainNoiseScale = 10f;
    [SerializeField] private int seed;
    private float terrainOffsetX;
    private float terrainOffsetY;

    [Header("Resource Noise")]
    [SerializeField] private float resourceNoiseScale = 15f;
    private float resourceOffsetX;
    private float resourceOffsetY;

    [Header("Sorting")]
    [SerializeField] private int sortingFactor = 100;
    [SerializeField] private int groundSortingOrder = -10000;

    [Header("Cells")]
    [SerializeField] private CellSpawnData[] cells;

    [Header("Resources")]
    [SerializeField] private ResourceSpawnData[] resources;

    private IGrid grid;

    public IGrid GetGrid()
    {
        return grid;
    }

    void Start()
    {
        GenerateAll();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GenerateAll();
        }
    }

    void GenerateAll()
    {
        ClearWorld();

        grid = new Grid2D(width, height, cellSize, Vector2.zero);

        GenerateSeed();
        GenerateWorldCells();

        if (bakeNavMesh2D != null)
            bakeNavMesh2D.Bake();
    }

    void ClearWorld()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void GenerateSeed()
    {
        System.Random prng = new System.Random(seed);

        terrainOffsetX = prng.Next(-100000, 100000);
        terrainOffsetY = prng.Next(-100000, 100000);

        resourceOffsetX = prng.Next(-100000, 100000);
        resourceOffsetY = prng.Next(-100000, 100000);
    }

    void GenerateWorldCells()
    {
        for (int x = -width / 2; x <= width / 2; x++)
        {
            for (int y = -height / 2; y <= height / 2; y++)
            {
                Vector2Int cellPos = new Vector2Int(x, y);
                if (!grid.IsInside(cellPos)) continue;

                float terrainNoise = GetTerrainPerlin(x, y);
                CellSpawnData cellData = PickCellByNoise(terrainNoise);
                if (cellData == null) continue;

                Vector2 worldPos2D = grid.CellToWorld(cellPos);
                Vector3 worldPos = new Vector3(worldPos2D.x, worldPos2D.y, 0f);

                GameObject cellObj = Instantiate(
                    cellData.prefab,
                    worldPos,
                    Quaternion.identity,
                    transform
                );

                SetGroundSorting(cellObj);
                TrySpawnResource(cellData, worldPos, x, y);
            }
        }
    }

    float GetTerrainPerlin(int x, int y)
    {
        float px = ((float)(x + width / 2) / width) * terrainNoiseScale + terrainOffsetX;
        float py = ((float)(y + height / 2) / height) * terrainNoiseScale + terrainOffsetY;
        return Mathf.PerlinNoise(px, py);
    }

    float GetResourcePerlin(int x, int y)
    {
        float px = ((float)(x + width / 2) / width) * resourceNoiseScale + resourceOffsetX;
        float py = ((float)(y + height / 2) / height) * resourceNoiseScale + resourceOffsetY;
        return Mathf.PerlinNoise(px, py);
    }

    CellSpawnData PickCellByNoise(float noise)
    {
        List<CellSpawnData> validCells = new List<CellSpawnData>();
        float totalChance = 0f;

        foreach (var cell in cells)
        {
            if (noise >= cell.minNoise && noise <= cell.maxNoise)
            {
                validCells.Add(cell);
                totalChance += cell.chance;
            }
        }

        if (validCells.Count == 0) return null;

        float roll = Random.value * totalChance;
        float current = 0f;

        foreach (var cell in validCells)
        {
            current += cell.chance;
            if (roll <= current)
                return cell;
        }

        return null;
    }

    void TrySpawnResource(CellSpawnData cell, Vector3 worldPos, int x, int y)
    {
        if (resources == null || resources.Length == 0) return;

        float resourceNoise = GetResourcePerlin(x, y);

        foreach (var resource in resources)
        {
            if (resourceNoise < resource.minNoise || resourceNoise > resource.maxNoise)
                continue;

            if (!IsResourceAllowedInCell(resource, cell.id))
                continue;

            if (Random.value > resource.spawnChance)
                continue;

            GameObject resourceObj = Instantiate(
                resource.prefab,
                worldPos,
                Quaternion.identity,
                transform
            );

            ApplyYSorting(resourceObj, worldPos.y);
            break;
        }
    }

    bool IsResourceAllowedInCell(ResourceSpawnData resource, string cellId)
    {
        if (resource.allowedCells == null || resource.allowedCells.Length == 0)
            return true;

        foreach (var id in resource.allowedCells)
        {
            if (id == cellId)
                return true;
        }

        return false;
    }

    void SetGroundSorting(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sortingOrder = groundSortingOrder;
    }

    void ApplyYSorting(GameObject obj, float worldY)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sortingOrder = -(int)(worldY * sortingFactor);
    }
}
