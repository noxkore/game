using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingButton : MonoBehaviour
{
    public MarketIten thisIten;

    public Image icon;
    public Text name_;
    public Text price;

    public void getIten(MarketIten iten)
    {
        thisIten = iten;
    }

    void Start()
    {
        icon.sprite = thisIten.itenImage;
        name_.text = thisIten.itenName;
    }

    // Update is called once per frame
    void Update()
    {
        price.text = thisIten.itenPrice.ToString();        
    }

    public void onClick()
    {
        // aqui ele iria diminuir a sua quantidade de itens no inventario por clique
        MarketHandler.Instance.DecreaseValue(thisIten);
        MarketHandler.Instance.IncreaseValue(thisIten);
    }
}
