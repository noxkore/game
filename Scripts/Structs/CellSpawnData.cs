using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellSpawnData
{
    public string id;

    public GameObject prefab;
    [Range(0f, 1f)] public float minNoise;
    [Range(0f, 1f)] public float maxNoise;
    public float chance;
}
