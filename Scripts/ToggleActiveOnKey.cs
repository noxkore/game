using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActiveOnKey : MonoBehaviour
{
    [Header("Objeto para ativar/desativar")]
    [SerializeField] private GameObject target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && target != null)
        {
            // Toggle: se estava ativo -> desativa, se estava desativado -> ativa
            target.SetActive(!target.activeSelf);
        }
    }
}