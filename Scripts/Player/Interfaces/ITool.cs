using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITool
{
    DamageType DamageType { get; }
    void Use();
}
