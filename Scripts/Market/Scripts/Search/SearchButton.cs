using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchButton : MonoBehaviour
{
    public SearchItens thisIten;

    public Image icon;
    public Text name_;
    public Text description;
    public Text price;

    public void getIten(SearchItens iten)
    {
        thisIten = iten;
    }

    void Start()
    {
        icon.sprite = thisIten.itenImage;
        name_.text = thisIten.itenName;
        description.text = thisIten.itenDescription;
    }

    // Update is called once per frame
    void Update()
    {
        price.text = thisIten.itenPrice.ToString();
    }

    public void onclick()
    {
        SearchManager.Instance.getSearch(thisIten, SearchManager.Instance.searchObject.GetComponent<Search>().searchesList);
    }
}
