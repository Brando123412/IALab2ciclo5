using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreeringBehavior : MonoBehaviour
{
    public Transform target;
    public float maxSpeed = 5f; // Velocidad m�xima del objeto
    public float slowingDistance = 5f; // Distancia a partir de la cual el objeto comenzar� a desacelerar
    public float stoppingDistance = 1f; // Distancia a partir de la cual el objeto se detendr�
    [SerializeField] float maxSpeedRotation = 5f;

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
        Look();
        transform.position += transform.forward * Time.deltaTime * desiredSpeed;
    }

    public void Look()
    {
        Vector3 targetDirection = target.position - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDirection.normalized), Time.deltaTime * maxSpeedRotation);
    }
}
