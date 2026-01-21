using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChestMachineInteraction : MonoBehaviour
{
    [SerializeField] private ChestMachine chestMachine;
    [SerializeField] private MachineInventoryUI inventoryUI;

    private bool playerInside;

    private void Awake()
    {
        if (chestMachine == null)
            chestMachine = GetComponent<ChestMachine>();

        inventoryUI.Close();
    }

    private void Start()
    {
        inventoryUI.Bind(chestMachine.Inventory);
    }

    private void Update()
    {
        if (!playerInside)
            return;

        if (Input.GetKeyDown(KeyCode.E))
            inventoryUI.Open();

        if (Input.GetKeyDown(KeyCode.Escape))
            inventoryUI.Close();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            inventoryUI.Close();
        }
    }
}