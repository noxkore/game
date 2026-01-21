using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketUI : MonoBehaviour
{
    public Canvas Canvas;

    public void close()
    {
        Canvas.gameObject.SetActive(false);
    }

}
