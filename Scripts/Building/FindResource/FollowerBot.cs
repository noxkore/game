using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowerBot : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private float x = 1;
    [SerializeField] private float y = 1;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void MoveTo(Vector2 target)
    {
        agent.SetDestination(target);
    }

    private void Update()
    {
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            if (agent.velocity.x > 0.05f)
                transform.localScale = new Vector3(x, y, 1);
            else if (agent.velocity.x < -0.05f)
                transform.localScale = new Vector3(-x, y, 1);
        }
    }
}