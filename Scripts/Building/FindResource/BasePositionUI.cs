using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import necessário para TMP_InputField

public class BasePositionUI : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GetterBrain getterBrain; // GetterBrain que será atualizado

    [SerializeField] private TMP_InputField inputX;  // Input TMP para X
    [SerializeField] private TMP_InputField inputY;  // Input TMP para Y

    // Define o GetterBrain que queremos atualizar (útil se houver vários)
    public void SetTargetBrain(GetterBrain brain)
    {
        getterBrain = brain;

        if (brain != null)
        {
            inputX.text = brain.basePosition.x.ToString();
            inputY.text = brain.basePosition.y.ToString();
        }
    }

    // Aplica os valores dos Inputs ao GetterBrain selecionado
    public void ApplyPosition()
    {
        if (getterBrain == null)
        {
            Debug.LogWarning("BasePositionUI: Nenhum GetterBrain atribuído!");
            return;
        }

        float x = getterBrain.basePosition.x;
        float y = getterBrain.basePosition.y;

        // Tenta converter os inputs em float
        if (!string.IsNullOrEmpty(inputX.text))
            float.TryParse(inputX.text, out x);

        if (!string.IsNullOrEmpty(inputY.text))
            float.TryParse(inputY.text, out y);

        // Atualiza apenas a posição X e Y, mantendo Z
        getterBrain.basePosition = new Vector3(x, y, getterBrain.basePosition.z);

        Debug.Log($"[BasePositionUI] Nova basePosition do {getterBrain.name}: {getterBrain.basePosition}");
    }
}