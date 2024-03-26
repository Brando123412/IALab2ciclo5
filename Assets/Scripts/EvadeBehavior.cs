using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeBehavior : MonoBehaviour
{
    public Transform target; // El objetivo al que el objeto debe evadir
    public float evasionDistance = 5f; // La distancia a la que se activar� la evasi�n
    public float evasionSpeed = 5f; // La velocidad a la que el objeto evadir� al objetivo

    void Update()
    {
        // Calcula la distancia al objetivo
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        Debug.Log(distanceToTarget + "-" + evasionSpeed);
        // Si el objetivo est� dentro de la distancia de evasi�n
        if (distanceToTarget <= evasionDistance)
        {
            // Calcula la direcci�n de evasi�n
            Vector3 evasionDirection = (transform.position - target.position).normalized;

            // Calcula la velocidad de evasi�n
            Vector3 evasionVelocity = evasionDirection * evasionSpeed * Time.deltaTime;

            // Calcula la nueva posici�n del objeto
            Vector3 newPosition = transform.position + evasionVelocity;

            // Asigna la nueva posici�n al objeto
            transform.position = newPosition;
        }
    }
}
