using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public GameObject toClose;

    public void CloseObject()
    {
        toClose.SetActive(false);
    }
}
