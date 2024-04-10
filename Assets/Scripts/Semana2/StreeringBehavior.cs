using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StreeringBehavior : MonoBehaviour
{
    public Transform target;
    public float maxSpeed = 5f; 
    public float slowingDistance = 5f; 
    public float stoppingDistance = 1f; 
    [SerializeField] float maxSpeedRotation = 5f;

    private NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void Arrive()
    {
        Vector3 targetDirection = target.position - transform.position;

        float distance = targetDirection.magnitude;

        float desiredSpeed = maxSpeed;

        if (distance < slowingDistance)
        {
            desiredSpeed = maxSpeed * (distance / slowingDistance);
        }

        
        if (distance < stoppingDistance)
        {
            desiredSpeed = 0f;
        }
        navMeshAgent.velocity = transform.forward * desiredSpeed;
        Look();
    }

    public void Look()
    {
        Vector3 targetDirection = target.position - transform.position;

        Quaternion desiredRotation = Quaternion.LookRotation(targetDirection.normalized);

        navMeshAgent.transform.rotation = Quaternion.Lerp(navMeshAgent.transform.rotation, desiredRotation, Time.deltaTime * maxSpeedRotation);
    }
}
