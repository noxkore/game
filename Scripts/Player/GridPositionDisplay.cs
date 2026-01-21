using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridPositionDisplay : MonoBehaviour
{
    [Header("Objeto e UI")]
    [SerializeField] private Transform target;      // Objeto que queremos monitorar
    [SerializeField] private TMP_Text coordinatesText; // TextMeshPro para mostrar coordenadas

    [Header("Configuração da Grid")]
    [SerializeField] private int gridWidth = 20;
    [SerializeField] private int gridHeight = 20;

    private Grid2D grid;

    private void Awake()
    {
        // Cria a Grid2D na hora
        // CellSize = 1, origem = Vector2.zero
        grid = new Grid2D(gridWidth, gridHeight, 1f, Vector2.zero);
    }

    private Vector2Int lastCellPos = new Vector2Int(int.MinValue, int.MinValue);

    private void Update()
    {
        if (target == null || coordinatesText == null)
            return;

        // Pega a célula atual do objeto
        Vector2Int cellPos = grid.WorldToCell(target.position);

        // Só atualiza o texto se mudou de célula
        if (cellPos != lastCellPos)
        {
            coordinatesText.text = $"X: {cellPos.x} | Y: {cellPos.y}";
            lastCellPos = cellPos;
        }
    }
}
