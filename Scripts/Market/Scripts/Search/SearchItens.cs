using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Search Iten", menuName ="Search Iten")]
public class SearchItens : ScriptableObject
{
    public string itenName;
    public string itenDescription;

    public int itenPrice;
    public Sprite itenImage;
    public GameObject itenObject;
}
