using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBreakable
{

    event System.Action<Vector2> OnDestroyed;
    void Suffer(float amount, SufferContext context);
    void Break();
}
