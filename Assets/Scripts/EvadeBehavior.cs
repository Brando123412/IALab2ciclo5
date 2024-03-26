using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeBehavior : MonoBehaviour
{
    public Transform target; // El objetivo al que el objeto debe evadir
    public float evasionDistance = 5f; // La distancia a la que se activará la evasión
    public float evasionSpeed = 5f; // La velocidad a la que el objeto evadirá al objetivo

    void Update()
    {
        // Calcula la distancia al objetivo
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        Debug.Log(distanceToTarget + "-" + evasionSpeed);
        // Si el objetivo está dentro de la distancia de evasión
        if (distanceToTarget <= evasionDistance)
        {
            // Calcula la dirección de evasión
            Vector3 evasionDirection = (transform.position - target.position).normalized;

            // Calcula la velocidad de evasión
            Vector3 evasionVelocity = evasionDirection * evasionSpeed * Time.deltaTime;

            // Calcula la nueva posición del objeto
            Vector3 newPosition = transform.position + evasionVelocity;

            // Asigna la nueva posición al objeto
            transform.position = newPosition;
        }
    }
}
