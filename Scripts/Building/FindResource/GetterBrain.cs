using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetterBrain : MonoBehaviour
{
    [Header("Search")]
    [SerializeField] private FindResource finder;
    [SerializeField] private float repathInterval = 0.5f;
    [SerializeField] private float arriveDistance = 0.2f;

    [Header("Drop Settings")]
    [SerializeField] private float dropOffsetRadius = 0.3f;

    [Header("Debug Base")]
    [SerializeField] private Vector3 basePosition;

    private FollowerBot mover;
    private BotAttack attacker;
    private GetterMachine machine;

    private Transform currentTarget;
    private float timer;
    private bool returningToBase;
    private bool hasDroppedThisTrip; // 

    private void Awake()
    {
        mover = GetComponent<FollowerBot>();
        attacker = GetComponent<BotAttack>();
        machine = GetComponent<GetterMachine>();

        basePosition = transform.position;
    }

    private void Update()
    {
        Vector2 currentPosition = transform.position;

        // Se o inventário está cheio, volta para base
        if (machine.IsStorageFull)
        {
            if (!returningToBase)
            {
                returningToBase = true;
                hasDroppedThisTrip = false; // reseta flag quando inicia a volta
                currentTarget = null;
                attacker.ClearTarget();
                mover.MoveTo(basePosition);
            }

            // Chegou na basedropa apenas uma vez
            if (Vector2.Distance(currentPosition, basePosition) <= arriveDistance && !hasDroppedThisTrip)
            {
                DropInventory();                   // instancia objetos
                machine.Inventory.ClearInventory(); // limpa inventário
                hasDroppedThisTrip = true;         // impede repetição
                returningToBase = false;           // volta a buscar recursos
            }

            return; 
        }

        if (!machine.IsStorageFull && returningToBase)
        {
            returningToBase = false;
            hasDroppedThisTrip = false;
        }
        timer -= Time.deltaTime;
        if (currentTarget == null && timer <= 0f)
        {
            timer = repathInterval;
            currentTarget = finder.FindClosest();

            if (currentTarget != null)
            {
                mover.MoveTo(currentTarget.position);
                attacker.SetTarget(currentTarget);
            }
        }
    }

    public void OnResourceDestroyed(Vector2 resourcePosition)
    {
        currentTarget = null;
        attacker.ClearTarget();
        mover.MoveTo(resourcePosition);
    }

    private void DropInventory()
    {
        int dropLayer = LayerMask.NameToLayer("Drop");

        int totalObjectsToDrop = 0;

        // Conta total de objetos que serão dropados
        foreach (var slot in machine.Inventory.Slots)
        {
            if (!slot.IsEmpty && slot.amount > 0 && slot.item.prefab != null)
            {
                totalObjectsToDrop += slot.amount;
            }
        }

        Debug.Log($"[GetterBrain] Inventário cheio! Slots com itens: {machine.Inventory.Slots.Count}, Objetos que serão dropados: {totalObjectsToDrop}");

        // Instancia os objetos
        foreach (var slot in machine.Inventory.Slots)
        {
            if (!slot.IsEmpty && slot.amount > 0 && slot.item.prefab != null)
            {
                for (int i = 0; i < slot.amount; i++)
                {
                    Vector2 offset = UnityEngine.Random.insideUnitCircle * dropOffsetRadius;
                    Vector3 dropPos = (Vector2)basePosition + offset;

                    GameObject go = Instantiate(slot.item.prefab, dropPos, Quaternion.identity);
                    SetLayerRecursively(go, dropLayer);
                }
            }
        }

        // Limpa o inventário depois de dropar
        machine.Inventory.ClearInventory();
    }

    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
            SetLayerRecursively(child.gameObject, layer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(basePosition, 0.2f);
    }
}