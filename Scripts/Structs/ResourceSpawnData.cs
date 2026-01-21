using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceSpawnData
{
    public string id;
    public GameObject prefab;

    [Range(0f, 1f)] public float minNoise;
    [Range(0f, 1f)] public float maxNoise;

    [Tooltip("Chance final de spawn quando o noise estiver válido")]
    [Range(0f, 1f)] public float spawnChance = 1f;

    [Tooltip("IDs de células onde este recurso pode spawnar")]
    public string[] allowedCells;
}