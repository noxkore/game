using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedTrigger : MonoBehaviour, ITrigger
{
    public event Action<IContext> OnTriggered;

    [Header("References")]
    [SerializeField] private ResourceHealthComponent targetHealth;

    [Header("Player Distance Filter")]
    [SerializeField] private float maxTriggerDistance = 8f;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float distanceFalloff = 2.5f;


    private Transform player;

    private FeedbackAction feedbackAction;
    private PlaySoundAction playSoundAction;

    protected virtual void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("HittedTrigger: Player não encontrado com a tag " + playerTag);
        }

        feedbackAction = GetComponent<FeedbackAction>();
        playSoundAction = GetComponent<PlaySoundAction>();

        if (targetHealth == null)
        {
            Debug.LogWarning("HittedTrigger: TargetHealth não atribuído, tentando buscar no mesmo GameObject");
            targetHealth = GetComponent<ResourceHealthComponent>();
        }

        if (targetHealth != null)
        {
            targetHealth.OnDamageTaken += OnHealthHitted;
        }
        else
        {
            Debug.LogError("HittedTrigger: Nenhum ResourceHealthComponent encontrado!");
        }
    }

    protected virtual void OnDestroy()
    {
        if (targetHealth != null)
        {
            targetHealth.OnDamageTaken -= OnHealthHitted;
        }
    }

    private void OnHealthHitted(float damageAmount)
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(
            player.position,
            targetHealth.transform.position
        );

        if (distance > maxTriggerDistance)
            return;

        float localIntensity = Mathf.InverseLerp(
            maxTriggerDistance,
            0f,
            distance
        );

        localIntensity = Mathf.Pow(localIntensity, distanceFalloff);


        if (feedbackAction != null)
        {
            feedbackAction.SetLocalIntensity(localIntensity);
        }

        if (playSoundAction != null)
        {
            playSoundAction.SetLocalIntensity(localIntensity);
        }

        IContext ctx = new SimpleContext
        {
            Amount = damageAmount,
            Target = targetHealth.gameObject
        };

        OnTriggered?.Invoke(ctx);
    }
}

public class SimpleContext : IContext
{
    public float Amount;
    public GameObject Target;
}