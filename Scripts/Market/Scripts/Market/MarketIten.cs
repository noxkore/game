using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Market Iten", menuName = "Market Iten")]

public class MarketIten : ScriptableObject
{
    public string itenName;
    
    public int itenPrice;

    public int itenNormalPrice;
    public int itenMaxPrice;
    public int itenMinPrice; 

    public Sprite itenImage;

    private void Awake()
    {
        itenPrice = itenNormalPrice;
    }
    
}

