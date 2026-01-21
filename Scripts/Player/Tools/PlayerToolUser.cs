using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerToolUser : MonoBehaviour
{
    [SerializeField] private ToolBase currentTool;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Usando Ferramenta (PlayerToolUser)");
            currentTool.Use();
        }
    }
}