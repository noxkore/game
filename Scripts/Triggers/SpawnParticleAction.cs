using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticleAction : ActionBase
{
    [Header("Particle Settings")]
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private float destroyAfter = 4f;

    public override void Execute(IContext context)
    {
        if (particlePrefab == null) return;

        Vector3 spawnPosition = transform.position;

        ParticleSystem particle =
            Instantiate(particlePrefab, spawnPosition, Quaternion.identity);

        particle.Play();

        Destroy(particle.gameObject, destroyAfter);
    }
}
