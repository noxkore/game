using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivateOnKey : MonoBehaviour
{
    [Header("Objeto a ser ativado")]
    [SerializeField] private GameObject objectToActivate;

    [Header("Tecla de ativação")]
    [SerializeField] private KeyCode activationKey = KeyCode.F;

    private bool playerInside = false; // Verifica se o player está dentro do trigger

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(activationKey))
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(!objectToActivate.activeSelf);
            }
            else
            {
                Debug.LogWarning("TriggerActivateOnKey: Nenhum objeto atribuído para ativar!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }
}