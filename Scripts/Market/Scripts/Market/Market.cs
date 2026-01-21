using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    public List<MarketIten> marketItens = new List<MarketIten>();

    public Image MarketCanvas;
    public GameObject sellingButtonPrefab;

    void createSellingButtons(List<MarketIten> itens)
    {
        foreach(MarketIten iten in itens)
        {
            GameObject newButton = Instantiate(sellingButtonPrefab, MarketCanvas.transform);
            newButton.GetComponent<SellingButton>().getIten(iten);
        }
    }
    private void Start()
    {
        createSellingButtons(marketItens);
        StartCoroutine(ChangePriceRoutine());
    }

    private IEnumerator ChangePriceRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            Market market = MarketHandler.Instance.marketObject.GetComponent<Market>();
            MarketHandler.Instance.ChangePrice(market.marketItens);
        }
    }
}
