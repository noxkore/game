using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivateObject : MonoBehaviour
{
    [Header("Objeto a ser ativado")]
    [SerializeField] private GameObject objectToActivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
            else
            {
                Debug.LogWarning("TriggerActivateObject: Nenhum objeto atribuído para ativar!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(false);
            }
            else
            {
                Debug.LogWarning("TriggerActivateObject: Nenhum objeto atribuído para ativar!");
            }
        }
    }
}