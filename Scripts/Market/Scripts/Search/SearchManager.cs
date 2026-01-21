using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SearchManager : MonoBehaviour
{
    public static SearchManager Instance { get; private set; }

    public GameObject searchObject;

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

    public void getSearch(SearchItens Iten, List<GameObject> searchesItens)
    { // precisa adicionar o negocio de verificar o dinheiro
        searchesItens.Add(Iten.itenObject);
    }

}
