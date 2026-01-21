using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketHandler : MonoBehaviour
{
    public static MarketHandler Instance { get; private set; }

    public GameObject marketObject;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    public void ShowMarketItens(List<MarketIten> marketList)
    {
        if (marketList == null || marketList.Count == 0)
        {
            Debug.LogWarning("Lista de itens do mercado está vazia ou nula.");
            return;
        }

        foreach (MarketIten iten in marketList)
        {
            Debug.Log($"{iten.itenName} - Preço: {iten.itenPrice}");
        }
    }
    public void IncreaseValue(MarketIten iten)
    {
        if (iten == null) return;

        foreach (MarketIten marketIten in marketObject
            .GetComponent<Market>()
            .marketItens)
        {
            // Pula o item que NÃO deve ser alterado
            if (marketIten.itenName == iten.itenName)
                continue;

            if (marketIten.itenPrice < marketIten.itenMaxPrice)
            {
                marketIten.itenPrice += 1;

                marketIten.itenPrice = Mathf.Clamp(
                    marketIten.itenPrice,
                    marketIten.itenMinPrice,
                    marketIten.itenMaxPrice
                );
            }
        }
    }


    public void DecreaseValue(MarketIten iten)
    {
        if (iten == null) return;

        int reducao = Random.Range(1, 10);
        iten.itenPrice -= reducao;

        iten.itenPrice = Mathf.Clamp(
            iten.itenPrice,
            iten.itenMinPrice,
            iten.itenMaxPrice
        );
    }


    public void ChangePrice(List<MarketIten> marketList)
    {
        foreach (MarketIten iten in marketList)
        {
            int escolha = Random.Range(0, 3); 

            if (escolha == 0)
            {
                iten.itenPrice = iten.itenNormalPrice;
            }
            else if (escolha == 1)
            {
                int aumento = Random.Range(1, 10);
                iten.itenPrice += aumento;
            }
            else
            {
                int reducao = Random.Range(1, 10);
                iten.itenPrice -= reducao;
            }

            iten.itenPrice = Mathf.Clamp(
                iten.itenPrice,
                iten.itenMinPrice,
                iten.itenMaxPrice
            );
        }
    }


}
