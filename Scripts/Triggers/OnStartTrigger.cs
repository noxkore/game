using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartTrigger : MonoBehaviour, ITrigger
{
    public event Action<IContext> OnTriggered;

    private void Start()
    {
       
        TriggerContext context = new TriggerContext(gameObject, null);
        OnTriggered?.Invoke(context);
        
    }
}
